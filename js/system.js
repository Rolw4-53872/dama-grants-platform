// نظام إدارة منصة داما المتكامل
class DAMASystem {
  constructor() {
    this.currentUser = null;
    this.applications = [];
    this.users = [];
    this.contracts = [];
    this.init();
  }

  init() {
    this.loadFromStorage();
    this.setupEventListeners();
    this.initializeDemo();
  }

  // تسجيل الدخول
  login(email, password) {
    const user = this.users.find(u => u.email === email && u.password === password);
    if (user) {
      this.currentUser = user;
      localStorage.setItem('currentUser', JSON.stringify(user));
      return { success: true, user };
    }
    return { success: false, message: 'بيانات دخول غير صحيحة' };
  }

  // تسجيل الخروج
  logout() {
    this.currentUser = null;
    localStorage.removeItem('currentUser');
  }

  // إنشاء حساب جديد
  register(data) {
    const newUser = {
      id: Date.now(),
      ...data,
      createdAt: new Date().toISOString(),
      role: 'association'
    };
    this.users.push(newUser);
    this.saveToStorage();
    return { success: true, user: newUser };
  }

  // إنشاء طلب جديد
  createApplication(data) {
    const application = {
      id: `REQ-${String(this.applications.length + 1).padStart(3, '0')}`,
      userId: this.currentUser.id,
      ...data,
      status: 'pending',
      createdAt: new Date().toISOString(),
      updatedAt: new Date().toISOString()
    };
    this.applications.push(application);
    this.saveToStorage();
    return { success: true, application };
  }

  // الحصول على طلبات المستخدم
  getUserApplications(userId = this.currentUser?.id) {
    return this.applications.filter(app => app.userId === userId);
  }

  // تحديث حالة الطلب
  updateApplicationStatus(appId, status, notes = '') {
    const app = this.applications.find(a => a.id === appId);
    if (app) {
      app.status = status;
      app.notes = notes;
      app.updatedAt = new Date().toISOString();
      this.saveToStorage();
      return { success: true, application: app };
    }
    return { success: false, message: 'الطلب غير موجود' };
  }

  // الحصول على إحصائيات
  getStatistics() {
    return {
      totalApplications: this.applications.length,
      pendingApplications: this.applications.filter(a => a.status === 'pending').length,
      approvedApplications: this.applications.filter(a => a.status === 'approved').length,
      rejectedApplications: this.applications.filter(a => a.status === 'rejected').length,
      totalUsers: this.users.length,
      totalContracts: this.contracts.length
    };
  }

  // حفظ البيانات في التخزين المحلي
  saveToStorage() {
    localStorage.setItem('damaSystem', JSON.stringify({
      users: this.users,
      applications: this.applications,
      contracts: this.contracts
    }));
  }

  // تحميل البيانات من التخزين المحلي
  loadFromStorage() {
    const data = localStorage.getItem('damaSystem');
    if (data) {
      const parsed = JSON.parse(data);
      this.users = parsed.users || [];
      this.applications = parsed.applications || [];
      this.contracts = parsed.contracts || [];
    }

    const user = localStorage.getItem('currentUser');
    if (user) {
      this.currentUser = JSON.parse(user);
    }
  }

  // تهيئة البيانات التجريبية
  initializeDemo() {
    if (this.users.length === 0) {
      // مستخدمون تجريبيون
      this.users = [
        {
          id: 1,
          name: 'جمعية البر بجدة',
          email: 'info@albir.org',
          password: 'password',
          role: 'association',
          city: 'جدة',
          license: '700123456'
        },
        {
          id: 2,
          name: 'مسؤول النظام',
          email: 'admin@dama.com',
          password: 'adminpass',
          role: 'admin'
        },
        {
          id: 3,
          name: 'المدير التنفيذي',
          email: 'ceo@dama.com',
          password: 'ceopass',
          role: 'executive'
        }
      ];

      // طلبات تجريبية
      this.applications = [
        {
          id: 'REQ-001',
          userId: 1,
          title: 'تمكين الأسر المنتجة',
          description: 'مشروع لتمكين الأسر من خلال توفير رأس مال',
          amount: 500000,
          status: 'pending',
          createdAt: '2026-06-15',
          updatedAt: '2026-06-29'
        },
        {
          id: 'REQ-002',
          userId: 1,
          title: 'برنامج محو الأمية',
          description: 'برنامج تعليمي للكبار',
          amount: 300000,
          status: 'approved',
          createdAt: '2026-06-10',
          updatedAt: '2026-06-25'
        }
      ];

      this.contracts = [
        {
          id: 'CTR-001',
          appId: 'REQ-002',
          status: 'signed',
          createdAt: '2026-06-26'
        }
      ];

      this.saveToStorage();
    }
  }

  // إعداد المستمعات
  setupEventListeners() {
    // تسجيل الدخول
    document.addEventListener('submitLogin', (e) => {
      const result = this.login(e.detail.email, e.detail.password);
      if (result.success) {
        window.location.href = this.redirectByRole(result.user.role);
      }
    });

    // إنشاء حساب
    document.addEventListener('submitRegister', (e) => {
      this.register(e.detail);
    });

    // إنشاء طلب
    document.addEventListener('submitApplication', (e) => {
      this.createApplication(e.detail);
    });
  }

  // إعادة التوجيه حسب الدور
  redirectByRole(role) {
    const redirects = {
      association: 'pages/06-dashboard.html',
      admin: 'pages/10-admin-dashboard.html',
      executive: 'pages/20-executive-dashboard.html'
    };
    return redirects[role] || 'pages/01-landing.html';
  }

  // الحصول على المستخدم الحالي
  getCurrentUser() {
    return this.currentUser;
  }

  // التحقق من المصادقة
  isAuthenticated() {
    return this.currentUser !== null;
  }

  // الحصول على حالة الطلب بالعربية
  getStatusLabel(status) {
    const labels = {
      pending: 'قيد المراجعة',
      approved: 'مقبول',
      rejected: 'مرفوض',
      modification_requested: 'يحتاج تعديل'
    };
    return labels[status] || status;
  }

  // الحصول على لون الحالة
  getStatusColor(status) {
    const colors = {
      pending: '#fbbf24',
      approved: '#2c694e',
      rejected: '#b23b34',
      modification_requested: '#f97316'
    };
    return colors[status] || '#74777f';
  }
}

// تهيئة النظام
const dama = new DAMASystem();

// وظائف مساعدة
function showNotification(message, type = 'success') {
  const notification = document.createElement('div');
  const colors = {
    success: '#dcfce7',
    error: '#fdf3f2',
    warning: '#f7eed9'
  };
  const textColors = {
    success: '#166534',
    error: '#b23b34',
    warning: '#b8842a'
  };

  notification.style.cssText = `
    position: fixed;
    top: 20px;
    right: 20px;
    background: ${colors[type]};
    color: ${textColors[type]};
    padding: 16px 24px;
    border-radius: 8px;
    border-left: 4px solid ${textColors[type]};
    z-index: 9999;
    animation: slideIn 0.3s ease;
  `;
  notification.textContent = message;
  document.body.appendChild(notification);

  setTimeout(() => notification.remove(), 3000);
}

function formatDate(dateString) {
  const options = { year: 'numeric', month: 'long', day: 'numeric' };
  return new Date(dateString).toLocaleDateString('ar-SA', options);
}

function formatCurrency(amount) {
  return new Intl.NumberFormat('ar-SA', {
    style: 'currency',
    currency: 'SAR'
  }).format(amount);
}
