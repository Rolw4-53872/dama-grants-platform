// أدوات مساعدة عامة

// نظام التخزين المحلي
const StorageManager = {
  set(key, value) {
    localStorage.setItem(key, JSON.stringify(value));
  },
  get(key) {
    const item = localStorage.getItem(key);
    return item ? JSON.parse(item) : null;
  },
  remove(key) {
    localStorage.removeItem(key);
  },
  clear() {
    localStorage.clear();
  }
};

// نظام التحقق من البيانات
const Validator = {
  email(email) {
    const regex = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
    return regex.test(email);
  },
  phone(phone) {
    const regex = /^(\+966|0)[0-9]{9}$/;
    return regex.test(phone);
  },
  password(password) {
    return password.length >= 6;
  },
  required(value) {
    return value && value.trim().length > 0;
  },
  minLength(value, min) {
    return value.length >= min;
  },
  maxLength(value, max) {
    return value.length <= max;
  }
};

// نظام التنبيهات
class AlertSystem {
  static success(message, duration = 3000) {
    this.show(message, 'success', duration);
  }

  static error(message, duration = 3000) {
    this.show(message, 'error', duration);
  }

  static warning(message, duration = 3000) {
    this.show(message, 'warning', duration);
  }

  static info(message, duration = 3000) {
    this.show(message, 'info', duration);
  }

  static show(message, type = 'info', duration = 3000) {
    const colors = {
      success: { bg: '#dcfce7', border: '#2c694e', text: '#166534' },
      error: { bg: '#fdf3f2', border: '#b23b34', text: '#b23b34' },
      warning: { bg: '#f7eed9', border: '#b8842a', text: '#b8842a' },
      info: { bg: '#e7f1f8', border: '#002045', text: '#002045' }
    };

    const color = colors[type] || colors.info;
    const alert = document.createElement('div');
    alert.style.cssText = `
      position: fixed;
      top: 20px;
      right: 20px;
      background: ${color.bg};
      color: ${color.text};
      border-left: 4px solid ${color.border};
      padding: 16px 24px;
      border-radius: 8px;
      z-index: 9999;
      font-family: 'Noto Sans Arabic', sans-serif;
      box-shadow: 0 4px 12px rgba(0,0,0,0.1);
      animation: slideInRight 0.3s ease;
      max-width: 400px;
    `;
    alert.textContent = message;
    document.body.appendChild(alert);

    setTimeout(() => {
      alert.style.animation = 'slideOutRight 0.3s ease';
      setTimeout(() => alert.remove(), 300);
    }, duration);
  }
}

// نظام التحميل
class LoadingSpinner {
  static show(message = 'جاري التحميل...') {
    const overlay = document.createElement('div');
    overlay.id = 'loading-overlay';
    overlay.style.cssText = `
      position: fixed;
      inset: 0;
      background: rgba(0,0,0,0.5);
      display: flex;
      align-items: center;
      justify-content: center;
      z-index: 10000;
      font-family: 'Noto Sans Arabic', sans-serif;
    `;

    overlay.innerHTML = `
      <div style="background: white; padding: 40px; border-radius: 12px; text-align: center;">
        <div style="width: 40px; height: 40px; margin: 0 auto 16px; border: 4px solid #e2e8f0; border-top-color: #002045; border-radius: 50%; animation: spin 0.8s linear infinite;"></div>
        <p style="color: #181c1e; margin: 0;">${message}</p>
      </div>
    `;

    document.body.appendChild(overlay);
  }

  static hide() {
    const overlay = document.getElementById('loading-overlay');
    if (overlay) overlay.remove();
  }
}

// نظام الجداول الديناميكية
class TableManager {
  static create(data, columns, options = {}) {
    const table = document.createElement('table');
    table.style.cssText = 'width: 100%; border-collapse: collapse;';

    // رأس الجدول
    const thead = document.createElement('thead');
    const headerRow = document.createElement('tr');
    headerRow.style.cssText = 'background: #f7fafc; border-bottom: 2px solid #e2e8f0;';

    columns.forEach(col => {
      const th = document.createElement('th');
      th.textContent = col.label;
      th.style.cssText = 'padding: 16px; text-align: right; font-weight: 600;';
      headerRow.appendChild(th);
    });
    thead.appendChild(headerRow);
    table.appendChild(thead);

    // جسم الجدول
    const tbody = document.createElement('tbody');
    data.forEach((row, index) => {
      const tr = document.createElement('tr');
      tr.style.cssText = 'border-bottom: 1px solid #e2e8f0;';

      columns.forEach(col => {
        const td = document.createElement('td');
        td.style.cssText = 'padding: 16px;';
        td.textContent = row[col.key] || '-';
        tr.appendChild(td);
      });

      tbody.appendChild(tr);
    });
    table.appendChild(tbody);

    return table;
  }
}

// نظام الفلترة والبحث
class FilterSystem {
  constructor(data) {
    this.originalData = data;
    this.filteredData = [...data];
  }

  search(query, fields) {
    const query_lower = query.toLowerCase();
    this.filteredData = this.originalData.filter(item => {
      return fields.some(field => {
        const value = item[field];
        return value && value.toString().toLowerCase().includes(query_lower);
      });
    });
    return this.filteredData;
  }

  filter(field, value) {
    this.filteredData = this.originalData.filter(item => item[field] === value);
    return this.filteredData;
  }

  reset() {
    this.filteredData = [...this.originalData];
    return this.filteredData;
  }
}

// نظام الأشكال (Forms)
class FormManager {
  static validate(form) {
    const inputs = form.querySelectorAll('[required]');
    let isValid = true;

    inputs.forEach(input => {
      if (!input.value || input.value.trim() === '') {
        input.style.borderColor = '#b23b34';
        isValid = false;
      } else {
        input.style.borderColor = '#e2e8f0';
      }
    });

    return isValid;
  }

  static getFormData(form) {
    const formData = new FormData(form);
    const data = {};
    formData.forEach((value, key) => {
      data[key] = value;
    });
    return data;
  }

  static reset(form) {
    form.reset();
    form.querySelectorAll('[required]').forEach(input => {
      input.style.borderColor = '#e2e8f0';
    });
  }
}

// نظام التاريخ والوقت
class DateUtils {
  static format(date, format = 'DD/MM/YYYY') {
    const d = new Date(date);
    const day = String(d.getDate()).padStart(2, '0');
    const month = String(d.getMonth() + 1).padStart(2, '0');
    const year = d.getFullYear();

    return format
      .replace('DD', day)
      .replace('MM', month)
      .replace('YYYY', year);
  }

  static getRelativeTime(date) {
    const d = new Date(date);
    const now = new Date();
    const diff = now - d;

    const minutes = Math.floor(diff / 60000);
    const hours = Math.floor(diff / 3600000);
    const days = Math.floor(diff / 86400000);

    if (minutes < 1) return 'للتو';
    if (minutes < 60) return `قبل ${minutes} دقيقة`;
    if (hours < 24) return `قبل ${hours} ساعة`;
    if (days < 7) return `قبل ${days} يوم`;
    return this.format(date);
  }
}

// نظام المال والعملات
class CurrencyUtils {
  static format(amount) {
    return new Intl.NumberFormat('ar-SA', {
      style: 'currency',
      currency: 'SAR'
    }).format(amount);
  }

  static formatNumber(number) {
    return new Intl.NumberFormat('ar-SA').format(number);
  }
}

// نظام التنقل
class Navigation {
  static goto(page) {
    window.location.href = page;
  }

  static redirect(role) {
    const routes = {
      association: 'pages/06-dashboard.html',
      admin: 'pages/10-admin-dashboard.html',
      executive: 'pages/20-executive-dashboard.html',
      guest: 'pages/01-landing.html'
    };
    this.goto(routes[role] || routes.guest);
  }

  static back() {
    window.history.back();
  }

  static checkAuth() {
    const user = StorageManager.get('currentUser');
    if (!user) {
      this.goto('pages/02-login.html');
      return false;
    }
    return true;
  }
}

// إضافة CSS للرسوم المتحركة
const style = document.createElement('style');
style.textContent = `
  @keyframes slideInRight {
    from {
      transform: translateX(400px);
      opacity: 0;
    }
    to {
      transform: translateX(0);
      opacity: 1;
    }
  }

  @keyframes slideOutRight {
    from {
      transform: translateX(0);
      opacity: 1;
    }
    to {
      transform: translateX(400px);
      opacity: 0;
    }
  }

  @keyframes spin {
    to {
      transform: rotate(360deg);
    }
  }

  @keyframes bounce {
    0%, 100% {
      transform: translateY(0);
    }
    50% {
      transform: translateY(-10px);
    }
  }
`;
document.head.appendChild(style);
