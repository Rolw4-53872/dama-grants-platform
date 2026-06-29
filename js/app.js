const App = {
  state: {
    portal: 'applicant',
    screen: 'home',
    qstep: 1,
    adminScreen: 'qual',
    mgrScreen: 'dash',
    qualStatus: 'review',
    projStatus: 'none',
    contractStatus: 'none',
    q1: null, q2: null, q3: null,
    adminAuthed: false, mgrAuthed: false,
    aEmail: '', aPass: '', aErr: false,
    mEmail: '', mPass: '', mErr: false,
    loginEmail: '', loginPass: '', loginError: false,
    form: {
      assoc: 'جمعية البر بجدة',
      license: '700123456',
      project: 'تمكين الأسر المنتجة'
    }
  },

  fileList: [
    'شهادة الترخيص', 'اللائحة الأساسية', 'المعلومات البنكية',
    'حساب التبرع', 'الملف التعريفي', 'الهوية البصرية',
    'الخطة الاستراتيجية', 'الخطة التشغيلية', 'القوائم المالية (سنتان)'
  ],

  init() {
    this.render();
    window.addEventListener('hashchange', () => this.handleHash());
    this.handleHash();
  },

  handleHash() {
    const hash = location.hash.slice(1);
    if (hash) {
      const parts = hash.split('/');
      if (parts[1]) {
        this.state.portal = parts[0] || 'applicant';
        this.state.screen = parts[1] || 'home';
        this.render();
      }
    }
  },

  navigate(screen, extra = {}) {
    Object.assign(this.state, { screen, ...extra });
    this.render();
    window.scrollTo({ top: 0, behavior: 'smooth' });
  },

  setPortal(portal) {
    this.state.portal = portal;
    this.render();
  },

  setState(updates) {
    Object.assign(this.state, updates);
    this.render();
  },

  setForm(key, value) {
    this.state.form[key] = value;
    this.render();
  },

  // Status info helpers
  qiInfo(s) {
    if (s === 'accepted') return { label: 'مقبول', color: '#174528', bg: '#e3efe7', cls: 'status-accepted' };
    if (s === 'rejected') return { label: 'مرفوض', color: '#b23b34', bg: '#fdf3f2', cls: 'status-rejected' };
    if (s === 'edit') return { label: 'بانتظار التعديل', color: '#b8842a', bg: '#f7eed9', cls: 'status-review' };
    return { label: 'قيد المراجعة', color: '#b8842a', bg: '#f7eed9', cls: 'status-review' };
  },

  piInfo(s) {
    if (s === 'accepted') return { label: 'مقبول', color: '#174528', bg: '#e3efe7', cls: 'status-accepted' };
    if (s === 'rejected') return { label: 'مرفوض', color: '#b23b34', bg: '#fdf3f2', cls: 'status-rejected' };
    if (s === 'review') return { label: 'قيد المراجعة', color: '#b8842a', bg: '#f7eed9', cls: 'status-review' };
    return { label: 'لم يُقدّم', color: '#9aa2b3', bg: '#eef1f6', cls: 'status-none' };
  },

  ciInfo(s) {
    if (s === 'approved') return { label: 'معتمد — مكتمل', color: '#174528', bg: '#e3efe7', cls: 'status-accepted' };
    if (s === 'signed') return { label: 'تم رفع الموقّع', color: '#b8842a', bg: '#f7eed9', cls: 'status-signed' };
    if (s === 'admin_up') return { label: 'بانتظار التوقيع', color: '#1A81BC', bg: '#e7f1f8', cls: 'status-info' };
    return { label: 'لم يُرفع', color: '#9aa2b3', bg: '#eef1f6', cls: 'status-none' };
  },

  // Login logic
  doLogin() {
    const S = this.state;
    const em = (S.loginEmail || '').trim().toLowerCase();
    const pw = (S.loginPass || '').trim();
    if (em === 'admin@dama-business.com' && pw === 'adminpass') {
      this.setState({ portal: 'admin', adminAuthed: true, adminScreen: 'qual', loginError: false });
    } else if (em === 'ceo@dama-business.com' && pw === 'execpass') {
      this.setState({ portal: 'manager', mgrAuthed: true, mgrScreen: 'dash', loginError: false });
    } else if (em.length > 0 && pw.length > 0) {
      this.setState({ portal: 'applicant', screen: 'intro', loginError: false });
    } else {
      this.setState({ loginError: true });
    }
  },

  adminSignIn() {
    const S = this.state;
    const em = (S.aEmail || '').trim().toLowerCase();
    const pw = (S.aPass || '').trim();
    if (em === 'admin@dama-business.com' && pw === 'adminpass') {
      this.setState({ adminAuthed: true, adminScreen: 'qual', aErr: false });
    } else {
      this.setState({ aErr: true });
    }
  },

  mgrSignIn() {
    const S = this.state;
    const em = (S.mEmail || '').trim().toLowerCase();
    const pw = (S.mPass || '').trim();
    if (em === 'ceo@dama-business.com' && pw === 'execpass') {
      this.setState({ mgrAuthed: true, mgrScreen: 'dash', mErr: false });
    } else {
      this.setState({ mErr: true });
    }
  },

  // Data generators
  qualRowsData() {
    const rows = [
      { id: 'TQ-2026-0125', assoc: 'جمعية البر بجدة', license: '700123456', date: '20/06/2024', status: this.state.qualStatus },
      { id: 'TQ-2026-0124', assoc: 'جمعية تنمية الأسرة', license: '700200111', date: '19/06/2024', status: 'review' },
      { id: 'TQ-2026-0123', assoc: 'جمعية الإحسان', license: '700300222', date: '18/06/2024', status: 'rejected' },
      { id: 'TQ-2026-0122', assoc: 'جمعية كافل', license: '700400333', date: '17/06/2024', status: 'accepted' },
      { id: 'TQ-2026-0121', assoc: 'جمعية رعاية', license: '700500444', date: '16/06/2024', status: 'review' },
    ];
    return rows.map(r => { const i = this.qiInfo(r.status); return { ...r, ...i }; });
  },

  projRowsData() {
    const rows = [
      { id: 'PRJ-2026-0098', project: 'تمكين الأسر المنتجة', assoc: 'جمعية البر بجدة', value: '300,000', status: this.state.projStatus === 'none' ? 'review' : this.state.projStatus },
      { id: 'PRJ-2026-0097', project: 'التعليم الرقمي', assoc: 'جمعية كافل', value: '180,000', status: 'review' },
      { id: 'PRJ-2026-0096', project: 'رعاية الأيتام', assoc: 'جمعية تنمية', value: '250,000', status: 'accepted' },
      { id: 'PRJ-2026-0095', project: 'تأهيل الشباب', assoc: 'جمعية رعاية', value: '120,000', status: 'rejected' },
    ];
    return rows.map(r => { const i = this.piInfo(r.status); return { ...r, ...i }; });
  },

  mgrRowsData() {
    const S = this.state;
    const base = [
      { assoc: 'جمعية البر بجدة', q: S.qualStatus, project: S.projStatus === 'none' ? '—' : 'تمكين الأسر المنتجة', p: S.projStatus, c: S.contractStatus },
      { assoc: 'جمعية كافل', q: 'accepted', project: 'التعليم الرقمي', p: 'accepted', c: 'approved' },
      { assoc: 'جمعية تنمية الأسرة', q: 'accepted', project: 'رعاية الأيتام', p: 'review', c: 'none' },
      { assoc: 'جمعية الإحسان', q: 'rejected', project: '—', p: 'none', c: 'none' },
      { assoc: 'جمعية رعاية', q: 'review', project: '—', p: 'none', c: 'none' },
    ];
    return base.map(r => {
      const qi = this.qiInfo(r.q), pi = this.piInfo(r.p), ci = this.ciInfo(r.c);
      let fl, fc, fbg;
      if (r.c === 'approved') { fl = 'مكتمل'; fc = '#174528'; fbg = '#e3efe7'; }
      else if (r.q === 'rejected' || r.p === 'rejected') { fl = 'مرفوض'; fc = '#b23b34'; fbg = '#fdf3f2'; }
      else { fl = 'جارٍ'; fc = '#b8842a'; fbg = '#f7eed9'; }
      return { ...r, qi, pi, ci, fl, fc, fbg };
    });
  },

  render() {
    const S = this.state;

    // Hide all pages first
    document.querySelectorAll('.page').forEach(p => p.classList.remove('active'));

    // Role switcher
    document.querySelectorAll('.role-btn').forEach(btn => {
      btn.classList.remove('role-active');
      btn.classList.add('role-inactive');
    });
    const activeRole = document.getElementById('role-' + S.portal);
    if (activeRole) { activeRole.classList.add('role-active'); activeRole.classList.remove('role-inactive'); }

    // Determine which page to show
    if (S.portal === 'admin') {
      if (!S.adminAuthed) {
        this.showPage('admin-login');
      } else {
        this.showPage('admin');
        this.renderAdmin();
      }
    } else if (S.portal === 'manager') {
      if (!S.mgrAuthed) {
        this.showPage('manager-login');
      } else {
        this.showPage('manager');
        this.renderManager();
      }
    } else {
      this.showPage(S.screen);
      if (S.screen === 'qform') this.renderQualForm();
      if (S.screen === 'account') this.renderAccount();
      if (S.screen === 'qualstatus') this.renderQualStatus();
      if (S.screen === 'projstatus') this.renderProjStatus();
      if (S.screen === 'contract') this.renderContract();
    }
  },

  showPage(id) {
    const page = document.getElementById('page-' + id);
    if (page) page.classList.add('active');
  },

  renderQualForm() {
    const S = this.state;
    document.querySelectorAll('.qstep').forEach(s => s.style.display = 'none');
    const step = document.getElementById('qstep-' + S.qstep);
    if (step) step.style.display = 'block';

    // Stepper dots
    for (let i = 1; i <= 3; i++) {
      const dot = document.getElementById('stepper-dot-' + i);
      const line = document.getElementById('stepper-line-' + i);
      if (dot) {
        dot.className = 'stepper-dot ' + (S.qstep > i ? 'stepper-done' : (S.qstep === i ? 'stepper-active' : 'stepper-pending'));
        dot.textContent = S.qstep > i ? '✓' : i;
      }
      if (line) {
        line.className = 'stepper-line ' + (S.qstep > i ? 'stepper-line-done' : 'stepper-line-pending');
      }
    }
    document.getElementById('qstep-label').textContent = 'الخطوة ' + S.qstep + ' من 3';
  },

  renderAccount() {
    const S = this.state;
    const qi = this.qiInfo(S.qualStatus);
    const pi = this.piInfo(S.projStatus);
    const ci = this.ciInfo(S.contractStatus);

    document.getElementById('account-assoc').textContent = S.form.assoc;
    document.getElementById('account-assoc-name').textContent = S.form.assoc;
    document.getElementById('account-license').textContent = 'جمعية مسجّلة · ترخيص ' + S.form.license;
    document.getElementById('account-req-count').textContent = S.projStatus !== 'none' ? 2 : 1;

    const qualBadge = document.getElementById('account-qual-badge');
    qualBadge.textContent = qi.label;
    qualBadge.style.color = qi.color;
    qualBadge.style.background = qi.bg;

    const projCard = document.getElementById('account-proj-card');
    const contractCard = document.getElementById('account-contract-card');
    projCard.style.display = S.projStatus !== 'none' ? 'flex' : 'none';
    contractCard.style.display = S.contractStatus !== 'none' ? 'flex' : 'none';

    if (S.projStatus !== 'none') {
      document.getElementById('account-proj-name').textContent = S.form.project;
      const projBadge = document.getElementById('account-proj-badge');
      projBadge.textContent = pi.label;
      projBadge.style.color = pi.color;
      projBadge.style.background = pi.bg;
    }
    if (S.contractStatus !== 'none') {
      const cBadge = document.getElementById('account-contract-badge');
      cBadge.textContent = ci.label;
      cBadge.style.color = ci.color;
      cBadge.style.background = ci.bg;
    }
  },

  renderQualStatus() {
    const S = this.state;
    const qi = this.qiInfo(S.qualStatus);
    const statusEl = document.getElementById('qualstatus-label');
    statusEl.textContent = qi.label;
    statusEl.style.color = qi.color;

    document.getElementById('qualstatus-review').style.display = (S.qualStatus === 'review' || S.qualStatus === 'edit') ? 'flex' : 'none';
    document.getElementById('qualstatus-accepted').style.display = S.qualStatus === 'accepted' ? 'block' : 'none';
    document.getElementById('qualstatus-rejected').style.display = S.qualStatus === 'rejected' ? 'block' : 'none';
  },

  renderProjStatus() {
    const S = this.state;
    const pi = this.piInfo(S.projStatus);
    document.getElementById('projstatus-name').textContent = S.form.project;
    const statusEl = document.getElementById('projstatus-label');
    statusEl.textContent = pi.label;
    statusEl.style.color = pi.color;

    document.getElementById('projstatus-review').style.display = S.projStatus === 'review' ? 'flex' : 'none';
    document.getElementById('projstatus-accepted').style.display = S.projStatus === 'accepted' ? 'block' : 'none';
    document.getElementById('projstatus-rejected').style.display = S.projStatus === 'rejected' ? 'block' : 'none';
  },

  renderContract() {
    const S = this.state;
    const ci = this.ciInfo(S.contractStatus);
    const statusEl = document.getElementById('contract-status-label');
    statusEl.textContent = ci.label;
    statusEl.style.color = ci.color;

    document.getElementById('contract-needs-sign').style.display = (S.contractStatus === 'admin_up' || S.contractStatus === 'none') ? 'block' : 'none';
    document.getElementById('contract-signed').style.display = S.contractStatus === 'signed' ? 'flex' : 'none';
    document.getElementById('contract-approved').style.display = S.contractStatus === 'approved' ? 'flex' : 'none';
  },

  renderAdmin() {
    const S = this.state;
    document.querySelectorAll('.admin-section').forEach(s => s.style.display = 'none');
    const section = document.getElementById('admin-' + S.adminScreen);
    if (section) section.style.display = 'block';

    // Active nav
    document.querySelectorAll('.admin-nav').forEach(n => n.classList.remove('sidebar-nav-active'));
    if (S.adminScreen === 'qual' || S.adminScreen === 'qdetail') {
      document.getElementById('admin-nav-qual')?.classList.add('sidebar-nav-active');
    }
    if (S.adminScreen === 'proj' || S.adminScreen === 'pdetail') {
      document.getElementById('admin-nav-proj')?.classList.add('sidebar-nav-active');
    }

    if (S.adminScreen === 'qual') this.renderAdminQualList();
    if (S.adminScreen === 'proj') this.renderAdminProjList();
    if (S.adminScreen === 'qdetail') this.renderAdminQualDetail();
    if (S.adminScreen === 'pdetail') this.renderAdminProjDetail();
  },

  renderAdminQualList() {
    const rows = this.qualRowsData();
    const tbody = document.getElementById('admin-qual-tbody');
    tbody.innerHTML = rows.map(r => `
      <div class="data-table-row" style="grid-template-columns:130px 1.5fr 120px 120px 130px;" onclick="App.setState({adminScreen:'qdetail'})">
        <span style="color:#9aa2b3;">${r.id}</span>
        <span style="font-weight:600;">${r.assoc}</span>
        <span style="color:#5b6273;">${r.license}</span>
        <span style="color:#9aa2b3;">${r.date}</span>
        <span><span class="badge" style="color:${r.color};background:${r.bg};">${r.label}</span></span>
      </div>
    `).join('');
  },

  renderAdminProjList() {
    const rows = this.projRowsData();
    const tbody = document.getElementById('admin-proj-tbody');
    tbody.innerHTML = rows.map(r => `
      <div class="data-table-row" style="grid-template-columns:130px 1.4fr 1fr 120px 130px;" onclick="App.setState({adminScreen:'pdetail'})">
        <span style="color:#9aa2b3;">${r.id}</span>
        <span style="font-weight:600;">${r.project}</span>
        <span style="color:#5b6273;">${r.assoc}</span>
        <span>${r.value}</span>
        <span><span class="badge" style="color:${r.color};background:${r.bg};">${r.label}</span></span>
      </div>
    `).join('');
  },

  renderAdminQualDetail() {
    const qi = this.qiInfo(this.state.qualStatus);
    document.getElementById('admin-qdetail-status').textContent = qi.label;
    document.getElementById('admin-qdetail-status').style.color = qi.color;
  },

  renderAdminProjDetail() {
    const S = this.state;
    const pi = this.piInfo(S.projStatus);
    const ci = this.ciInfo(S.contractStatus);

    document.getElementById('admin-pdetail-status').textContent = pi.label;
    document.getElementById('admin-pdetail-status').style.color = pi.color;

    document.getElementById('admin-proj-review').style.display = S.projStatus === 'review' || S.projStatus === 'none' ? 'block' : 'none';
    document.getElementById('admin-proj-accepted').style.display = S.projStatus === 'accepted' ? 'block' : 'none';

    if (S.projStatus === 'accepted') {
      document.getElementById('admin-contract-status').textContent = ci.label;
      document.getElementById('admin-contract-status').style.color = ci.color;
      document.getElementById('admin-contract-none').style.display = S.contractStatus === 'none' ? 'block' : 'none';
      document.getElementById('admin-contract-admin-up').style.display = S.contractStatus === 'admin_up' ? 'block' : 'none';
      document.getElementById('admin-contract-signed').style.display = S.contractStatus === 'signed' ? 'block' : 'none';
      document.getElementById('admin-contract-approved').style.display = S.contractStatus === 'approved' ? 'block' : 'none';
    }
  },

  renderManager() {
    const S = this.state;
    document.querySelectorAll('.mgr-section').forEach(s => s.style.display = 'none');
    const section = document.getElementById('mgr-' + S.mgrScreen);
    if (section) section.style.display = 'block';

    document.querySelectorAll('.mgr-nav').forEach(n => n.classList.remove('sidebar-nav-active'));
    document.getElementById('mgr-nav-' + S.mgrScreen)?.classList.add('sidebar-nav-active');

    if (S.mgrScreen === 'table') this.renderMgrTable();
  },

  renderMgrTable() {
    const rows = this.mgrRowsData();
    const tbody = document.getElementById('mgr-table-tbody');
    tbody.innerHTML = rows.map(r => `
      <div class="mgr-table-row" style="grid-template-columns:1.3fr 110px 1.2fr 110px 120px 110px;">
        <span style="font-weight:600;">${r.assoc}</span>
        <span><span class="badge-sm" style="color:${r.qi.color};background:${r.qi.bg};">${r.qi.label}</span></span>
        <span style="color:#5b6273;">${r.project}</span>
        <span><span class="badge-sm" style="color:${r.pi.color};background:${r.pi.bg};">${r.pi.label}</span></span>
        <span><span class="badge-sm" style="color:${r.ci.color};background:${r.ci.bg};">${r.ci.label}</span></span>
        <span><span class="badge-sm" style="color:${r.fc};background:${r.fbg};">${r.fl}</span></span>
      </div>
    `).join('');
  }
};

document.addEventListener('DOMContentLoaded', () => App.init());
