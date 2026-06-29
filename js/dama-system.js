/**
 * نظام منصة داما المتكامل
 * DAMA Integrated Grant Management System
 *
 * نظام متقدم لإدارة المنح والتأهيل المؤسسي
 */

class DAMASystem {
  constructor() {
    this.currentUser = null;
    this.database = {};
    this.permissions = {};
    this.workflow = {};
    this.notifications = [];
    this.auditLog = [];
    this.init();
  }

  /**
   * تهيئة النظام
   */
  async init() {
    console.log('🚀 تهيئة نظام داما...');
    await this.loadDatabase();
    this.setupWorkflow();
    console.log('✅ تم تهيئة النظام بنجاح');
  }

  /**
   * تحميل قاعدة البيانات
   */
  async loadDatabase() {
    try {
      const response = await fetch('db/database.json');
      this.database = await response.json();
      console.log('✅ تم تحميل قاعدة البيانات');
    } catch (error) {
      console.error('❌ خطأ في تحميل قاعدة البيانات:', error);
      // استخدام بيانات تجريبية
      this.database = this.getDemoDatabase();
    }
  }

  /**
   * نظام تسجيل الدخول
   */
  login(email, password) {
    const user = this.database.users.find(u => u.email === email && u.password === password);

    if (!user) {
      return {
        success: false,
        message: 'بيانات دخول غير صحيحة'
      };
    }

    this.currentUser = {
      ...user,
      permissions: this.getUserPermissions(user.role)
    };

    this.logAudit('login', 'user', user.id, 'تسجيل دخول');
    localStorage.setItem('damaCurrentUser', JSON.stringify(this.currentUser));

    return {
      success: true,
      user: this.currentUser,
      redirectUrl: this.getRedirectUrlByRole(user.role)
    };
  }

  /**
   * تسجيل الخروج
   */
  logout() {
    if (this.currentUser) {
      this.logAudit('logout', 'user', this.currentUser.id, 'تسجيل خروج');
    }
    this.currentUser = null;
    localStorage.removeItem('damaCurrentUser');
  }

  /**
   * الحصول على صلاحيات المستخدم
   */
  getUserPermissions(roleSlug) {
    const role = this.database.roles.find(r => r.slug === roleSlug);
    return role ? role.permissions : [];
  }

  /**
   * التحقق من الصلاحية
   */
  hasPermission(permission) {
    if (!this.currentUser) return false;
    const perms = this.currentUser.permissions;
    return perms.includes('*') || perms.includes(permission);
  }

  /**
   * إنشاء طلب منحة جديد
   */
  submitApplication(appData) {
    if (!this.hasPermission('submit_application')) {
      return { success: false, message: 'ليس لديك صلاحية لتقديم طلب' };
    }

    const newApp = {
      id: `APP-${String(this.database.applications.length + 1).padStart(3, '0')}`,
      societyId: this.currentUser.id,
      ...appData,
      status: 'pending_review',
      submittedAt: new Date().toISOString(),
      workflow: {
        admin_review: { status: 'pending' },
        technical_review: { status: 'pending' },
        financial_review: { status: 'pending' },
        committee_decision: { status: 'pending' },
        director_approval: { status: 'pending' }
      },
      score: 0,
      decision: null,
      history: []
    };

    this.database.applications.push(newApp);
    this.triggerWorkflow('new_application', newApp);
    this.addNotification(this.currentUser.id, 'تم استقبال طلبك', 'تم استلام طلبك بنجاح وهو الآن قيد المراجعة', 'info');
    this.logAudit('submit_application', 'application', newApp.id, 'تقديم طلب منحة جديد');

    return { success: true, application: newApp };
  }

  /**
   * مراجعة الطلب (من قبل المراجعين)
   */
  reviewApplication(appId, reviewType, rating, comments) {
    if (!this.currentUser) {
      return { success: false, message: 'يجب تسجيل الدخول أولاً' };
    }

    const app = this.database.applications.find(a => a.id === appId);
    if (!app) {
      return { success: false, message: 'الطلب غير موجود' };
    }

    // تحديث مرحلة المراجعة
    app.workflow[reviewType] = {
      status: 'completed',
      reviewer: this.currentUser.email,
      rating: rating,
      comments: comments,
      completedAt: new Date().toISOString()
    };

    app.history.push({
      action: 'review_completed',
      reviewer: this.currentUser.name,
      type: reviewType,
      rating: rating,
      timestamp: new Date().toISOString()
    });

    // تحديث الدرجة
    app.score = this.calculateApplicationScore(app);

    // التحقق من انتهاء جميع المراجعات
    this.checkWorkflowCompletion(app);

    this.logAudit('review_application', 'application', appId, `مراجعة ${reviewType}`);

    return { success: true, application: app };
  }

  /**
   * اتخاذ قرار نهائي (لجنة التقييم)
   */
  makeDecision(appId, decision, comments) {
    if (!this.hasPermission('make_decision')) {
      return { success: false, message: 'ليس لديك صلاحية اتخاذ القرارات' };
    }

    const app = this.database.applications.find(a => a.id === appId);
    if (!app) return { success: false, message: 'الطلب غير موجود' };

    app.decision = decision;
    app.status = decision === 'approved' ? 'approved' : 'rejected';
    app.workflow.committee_decision = {
      status: 'completed',
      reviewer: this.currentUser.email,
      decision: decision,
      comments: comments,
      completedAt: new Date().toISOString()
    };

    app.history.push({
      action: 'decision_made',
      decision: decision,
      comments: comments,
      by: this.currentUser.name,
      timestamp: new Date().toISOString()
    });

    // إرسال إشعار للجمعية
    const message = decision === 'approved'
      ? 'تمت الموافقة على طلبك'
      : 'تم رفض طلبك';
    const type = decision === 'approved' ? 'success' : 'error';
    this.addNotification(app.societyId, message, `${message}: ${app.title}`, type);

    this.logAudit('make_decision', 'application', appId, `اتخاذ قرار: ${decision}`);

    return { success: true, application: app };
  }

  /**
   * إنشاء عقد (تلقائياً بعد الموافقة)
   */
  createContract(appId) {
    const app = this.database.applications.find(a => a.id === appId);
    if (!app || app.decision !== 'approved') {
      return { success: false, message: 'لا يمكن إنشاء عقد لهذا الطلب' };
    }

    const contract = {
      id: `CTR-${String(this.database.contracts.length + 1).padStart(3, '0')}`,
      applicationId: appId,
      societyId: app.societyId,
      amount: app.requestedAmount,
      status: 'pending_signature',
      createdAt: new Date().toISOString(),
      paymentSchedule: this.generatePaymentSchedule(app.requestedAmount, app.duration)
    };

    this.database.contracts.push(contract);
    this.addNotification(app.societyId, 'عقد جديد جاهز للتوقيع', 'تم إنشاء عقد جديد وهو جاهز للتوقيع الإلكتروني', 'info');
    this.logAudit('create_contract', 'contract', contract.id, 'إنشاء عقد جديد');

    return { success: true, contract };
  }

  /**
   * توقيع العقد من قبل الجمعية
   */
  signContract(contractId) {
    const contract = this.database.contracts.find(c => c.id === contractId);
    if (!contract) {
      return { success: false, message: 'العقد غير موجود' };
    }

    contract.status = 'signed_by_society';
    contract.signedByAt = new Date().toISOString();

    this.addNotification(this.currentUser.id, 'تم توقيع العقد', 'تم توقيع العقد بنجاح من قبلكم', 'success');
    this.logAudit('sign_contract', 'contract', contractId, 'توقيع العقد من الجمعية');

    return { success: true, contract };
  }

  /**
   * معالجة الدفعة (من قبل مسؤول الدفعات)
   */
  processPayment(contractId, phaseNumber) {
    if (!this.hasPermission('manage_payments')) {
      return { success: false, message: 'ليس لديك صلاحية معالجة الدفعات' };
    }

    const contract = this.database.contracts.find(c => c.id === contractId);
    if (!contract) return { success: false, message: 'العقد غير موجود' };

    const phase = contract.paymentSchedule.find(p => p.phase === phaseNumber);
    if (!phase || phase.status === 'completed') {
      return { success: false, message: 'لا يمكن معالجة هذه الدفعة' };
    }

    const payment = {
      id: `PAY-${String(this.database.payments.length + 1).padStart(3, '0')}`,
      contractId: contractId,
      phase: phaseNumber,
      amount: phase.amount,
      status: 'completed',
      processedAt: new Date().toISOString(),
      reference: `TRF-${this.generateReference()}`,
      processedBy: this.currentUser.email
    };

    this.database.payments.push(payment);
    phase.status = 'completed';

    const society = this.database.societies.find(s => s.id === contract.societyId);
    this.addNotification(contract.societyId, 'تم صرف الدفعة', `تم صرف الدفعة ${phaseNumber} بنجاح`, 'success');
    this.logAudit('process_payment', 'payment', payment.id, `معالجة الدفعة ${phaseNumber}`);

    return { success: true, payment };
  }

  /**
   * حساب درجة الطلب
   */
  calculateApplicationScore(app) {
    let score = 0;
    let count = 0;

    Object.keys(app.workflow).forEach(key => {
      if (app.workflow[key].rating) {
        score += app.workflow[key].rating;
        count++;
      }
    });

    return count > 0 ? Math.round(score / count) : 0;
  }

  /**
   * توليد جدول الدفعات
   */
  generatePaymentSchedule(amount, months) {
    const phases = [];
    const phaseAmount = Math.floor(amount / months);
    const remaining = amount % months;

    for (let i = 1; i <= months; i++) {
      const dueDate = new Date();
      dueDate.setMonth(dueDate.getMonth() + i);

      phases.push({
        phase: i,
        amount: i === months ? phaseAmount + remaining : phaseAmount,
        dueDate: dueDate.toISOString().split('T')[0],
        status: 'pending'
      });
    }

    return phases;
  }

  /**
   * إضافة إشعار
   */
  addNotification(userId, title, message, type = 'info') {
    const notification = {
      id: `NOT-${String(this.notifications.length + 1).padStart(3, '0')}`,
      userId: userId,
      title: title,
      message: message,
      type: type,
      read: false,
      createdAt: new Date().toISOString()
    };

    this.notifications.push(notification);
    this.database.notifications.push(notification);
  }

  /**
   * تسجيل في سجل التدقيق
   */
  logAudit(action, entity, entityId, details) {
    const log = {
      id: `AUD-${String(this.auditLog.length + 1).padStart(5, '0')}`,
      userId: this.currentUser ? this.currentUser.id : null,
      userName: this.currentUser ? this.currentUser.name : 'نظام',
      action: action,
      entity: entity,
      entityId: entityId,
      details: details,
      timestamp: new Date().toISOString(),
      ipAddress: this.getClientIP()
    };

    this.auditLog.push(log);
    this.database.auditLog.push(log);
  }

  /**
   * الحصول على عنوان IP (محاكاة)
   */
  getClientIP() {
    return '192.168.' + Math.floor(Math.random() * 256) + '.' + Math.floor(Math.random() * 256);
  }

  /**
   * توليد رقم مرجع
   */
  generateReference() {
    const now = new Date();
    const date = now.getFullYear().toString().slice(-2) +
                 String(now.getMonth() + 1).padStart(2, '0') +
                 String(now.getDate()).padStart(2, '0');
    const sequence = String(Math.floor(Math.random() * 10000)).padStart(4, '0');
    return `${date}-${sequence}`;
  }

  /**
   * إعادة التوجيه حسب الدور
   */
  getRedirectUrlByRole(role) {
    const redirects = {
      'association': 'dashboard-society.html',
      'admin_reviewer': 'dashboard-admin.html',
      'technical_reviewer': 'dashboard-reviewer.html',
      'financial_reviewer': 'dashboard-financial.html',
      'evaluation_committee': 'dashboard-committee.html',
      'executive_director': 'dashboard-executive.html',
      'contracts_manager': 'dashboard-contracts.html',
      'payments_manager': 'dashboard-payments.html',
      'system_admin': 'dashboard-system.html',
      'super_admin': 'dashboard-admin.html'
    };
    return redirects[role] || 'index.html';
  }

  /**
   * إعداد محرك سير العمل
   */
  setupWorkflow() {
    this.workflow = {
      'new_application': {
        trigger: 'عند استلام طلب جديد',
        steps: [
          'إرسال إشعار للمراجع الإداري',
          'إرسال إشعار للمراجع الفني',
          'إرسال إشعار للمراجع المالي'
        ]
      },
      'application_approved': {
        trigger: 'عند الموافقة على الطلب',
        steps: [
          'إنشاء عقد تلقائي',
          'إرسال العقد للجمعية',
          'إرسال إشعار للجمعية'
        ]
      },
      'application_rejected': {
        trigger: 'عند رفض الطلب',
        steps: [
          'إرسال رسالة الرفض للجمعية',
          'حفظ أسباب الرفض'
        ]
      }
    };
  }

  /**
   * تشغيل محرك سير العمل
   */
  triggerWorkflow(workflowName, data) {
    const workflow = this.workflow[workflowName];
    if (!workflow) return;

    console.log(`🔄 تشغيل سير عمل: ${workflowName}`);
    workflow.steps.forEach(step => {
      console.log(`  ✓ ${step}`);
    });
  }

  /**
   * التحقق من انتهاء المراجعات
   */
  checkWorkflowCompletion(app) {
    const allCompleted = Object.values(app.workflow)
      .filter(w => w.status !== undefined)
      .every(w => w.status === 'completed');

    if (allCompleted && app.workflow.committee_decision.status !== 'completed') {
      console.log('✅ تمت جميع المراجعات - في انتظار قرار اللجنة');
    }
  }

  /**
   * الحصول على بيانات تجريبية
   */
  getDemoDatabase() {
    return {
      roles: [],
      users: [],
      societies: [],
      programs: [],
      applications: [],
      contracts: [],
      payments: [],
      notifications: [],
      auditLog: []
    };
  }

  /**
   * الحصول على الإحصائيات
   */
  getStatistics() {
    return {
      totalApplications: this.database.applications.length,
      approvedApplications: this.database.applications.filter(a => a.decision === 'approved').length,
      rejectedApplications: this.database.applications.filter(a => a.decision === 'rejected').length,
      pendingApplications: this.database.applications.filter(a => a.status === 'pending_review').length,
      totalSocieties: this.database.societies.length,
      totalGrantsDistributed: this.database.applications
        .filter(a => a.decision === 'approved')
        .reduce((sum, a) => sum + a.requestedAmount, 0)
    };
  }
}

// تهيئة النظام
const dama = new DAMASystem();

// تصدير للاستخدام
if (typeof module !== 'undefined' && module.exports) {
  module.exports = DAMASystem;
}
