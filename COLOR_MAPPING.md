# جدول المقارنة - الألوان القديمة vs الجديدة

## الألوان الأساسية المستخدمة

### النظام القديم ❌
| الاستخدام | اللون القديم | الوصف |
|----------|----------|--------|
| Primary | `#272d68` | Navy غير متسق |
| Secondary | `#1A81BC` | أزرق متوسط |
| Tertiary | `#174528` | أخضر |
| Background | `#eef1f6` | رمادي قديم |
| Text Primary | `#272d68` | نفس Primary |
| Text Secondary | `#5b6273` | رمادي قديم |

### النظام الجديد ✅
| الاستخدام | اللون الجديد | الوصف |
|----------|----------|--------|
| Primary | `#00091b` | Navy عميق احترافي |
| Primary Container | `#002045` | Navy أفتح للخلفيات |
| Secondary | `#2c694e` | أخضر غابي متناسق |
| On Primary Container | `#7089b3` | أزرق فاتح للنصوص |
| Background | `#f7fafc` | أزرق رمادي فاتح |
| Text Primary | `#181c1e` | رمادي داكن |
| Text Secondary | `#44474e` | رمادي متوسط |

## الخطوط المستخدمة

### النظام القديم ❌
```css
font-family: 'Tajawal', sans-serif;
```
- خط عربي واحد فقط
- غير مناسب للعناوين الكبيرة

### النظام الجديد ✅
```css
font-family: 'Cairo', sans-serif; /* للعناوين */
font-family: 'Noto Sans Arabic', sans-serif; /* للنصوص */
```
- عاائلة ثنائية متخصصة
- Cairo للعناوين والنصوص الثقيلة
- Noto Sans Arabic للنصوص العادية

## المكونات الأساسية

### الأزرار

#### الزر الأساسي (Primary Button)

**قديم:**
```css
background: #272d68;
color: #fff;
border-radius: 9px;
```

**جديد:**
```css
background: var(--primary-container); /* #002045 */
color: var(--on-primary); /* #ffffff */
border-radius: 8px;
box-shadow: 0 4px 20px -2px rgba(0, 32, 69, 0.08);
transition: all 0.2s;
```

### حقول الإدخال

**قديم:**
```css
border: 1px solid #d9dee8;
border-radius: 9px;
color: #272d68;
```

**جديد:**
```css
border: 1px solid var(--outline); /* #e2e8f0 */
border-radius: 8px;
color: var(--on-surface); /* #181c1e */
background: var(--surface); /* #ffffff */
transition: border-color 0.2s;
```

### البطاقات (Cards)

**قديم:**
```css
background: #fff;
border-radius: 16px;
box-shadow: 0 8px 36px rgba(39,45,104,.1);
```

**جديد:**
```css
background: var(--surface); /* #ffffff */
border-radius: 16px;
border: 1px solid var(--outline); /* #e2e8f0 */
box-shadow: 0 4px 20px -2px rgba(0, 32, 69, 0.08);
```

### الشريط الجانبي

**قديم:**
```css
background: linear-gradient(180deg, #1A81BC, #272d68 50%, #174528);
```

**جديد:**
```css
background: linear-gradient(180deg, var(--primary-container), var(--primary) 50%, var(--secondary));
/* linear-gradient(180deg, #002045, #00091b 50%, #2c694e) */
```

## ألوان الحالات

### الحالة الخضراء (Success)

**قديم & جديد:** `#e3efe7` و `#174528` (بقيت نفسها)

### حالة المراجعة (Review/Warning)

**قديم & جديد:** `#f7eed9` و `#b8842a` (بقيت نفسها)

### حالة الرفض (Error)

**قديم & جديد:** `#fdf3f2` و `#b23b34` (بقيت نفسها)

## نظام الظلال

### الظل الأساسي (Institutional Shadow)

**قديم:**
```css
box-shadow: 0 8px 36px rgba(39,45,104,.1);
```

**جديد:**
```css
box-shadow: 0 4px 20px -2px rgba(0, 32, 69, 0.08);
```

**الفرق:**
- أكثر دقة وأقل ثقلاً
- استخدام ألوان navy بدلاً من الأزرق العام
- أكثر احترافية وحداثة

## الخلفيات والأنماط

### خلفية الصفحة

**قديم:**
```css
background: #eef1f6;
```

**جديد:**
```css
background: #f7fafc;
background-image: radial-gradient(#00204505 1px, transparent 0);
background-size: 24px 24px;
```

**التحسينات:**
- لون فاتح أكثر وتناسقاً
- إضافة نمط دقاط لإضافة عمق
- نمط يعكس الهوية البصرية الحديثة

## نسب التباين والوضوح

### تحسينات الوضوح

| العنصر | القديم | الجديد | التحسن |
|--------|--------|--------|--------|
| النص على Navy | قد يكون ضعيفاً | `#7089b3` واضح جداً | ✅ محسّن |
| النص الثانوي | `#5b6273` | `#44474e` | ✅ أفضل تباين |
| الحدود | `#d9dee8` | `#e2e8f0` | ✅ أكثر وضوحاً |

## الملخص

التحسينات الرئيسية:
1. ✅ **اتساق لوني**: نظام Material Design 3
2. ✅ **احترافية**: ألوان navy و green متناسقة
3. ✅ **وضوح**: ظلال وحدود محسّنة
4. ✅ **حداثة**: خطوط وتأثيرات عصرية
5. ✅ **إمكانية الوصول**: تباين أفضل للقراءة

---
**ملاحظة**: جميع الألوان الجديدة مأخوذة من الملف المرجعي `stitch_ (2).zip`
