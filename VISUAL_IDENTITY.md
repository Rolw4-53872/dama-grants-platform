# هوية داما البصرية المحدثة

## نظام الألوان الأساسي

### الألوان الأساسية
- **Primary Container**: `#002045` - Navy (الأزرق البحري الداكن - الاستخدام الأساسي)
- **Primary**: `#00091b` - Dark Navy (الأزرق الداكن الأعمق)
- **Secondary**: `#2c694e` - Forest Green (الأخضر الغابي - للتأكيد والحالات الثانوية)
- **Tertiary**: `#00081a` - Very Dark Navy
- **On Primary Container**: `#7089b3` - Light Blue (للنصوص على الخلفيات الأساسية)

### الألوان الخاصة بالحالات والحدود
- **On Surface**: `#181c1e` - Dark Gray (النصوص الأساسية)
- **On Surface Variant**: `#44474e` - Medium Gray (النصوص الثانوية)
- **Outline**: `#e2e8f0` - Light Gray (للحدود)
- **Surface**: `#ffffff` - White (الخلفيات البيضاء)
- **Surface Container Low**: `#f1f4f6` - Very Light Gray (الخلفيات الفاتحة)
- **Background**: `#f7fafc` - Lightest Blue-Gray (خلفية الصفحة)

### ألوان الحالات
- **Success**: `#e3efe7` & `#174528` (أخضر - للقبول والنجاح)
- **Warning**: `#f7eed9` & `#b8842a` (برتقالي - للمراجعة والتنبيهات)
- **Error**: `#fdf3f2` & `#b23b34` (أحمر - للرفض والأخطاء)
- **Info**: `#e7f1f8` & `#002045` (أزرق - للمعلومات)

## التايبوغرافيا

### الخطوط المستخدمة
- **العناوين**: `Cairo` - للعناوين الرئيسية والعناوين الكبيرة
- **النصوص**: `Noto Sans Arabic` - لجميع النصوص العادية والنصوص الثانوية
- **الوزن الأساسي**: 400 (عادي) و 500 (متوسط) و 700 (غامق) و 800-900 (جداً غامق)

## عناصر التصميم

### الزوايا المستديرة (Border Radius)
- Small: `0.25rem` (4px) - للعناصر الصغيرة
- Default: `0.5rem` (8px) - للعناصر العادية
- Large: `0.75rem` (12px) - للعناصر الكبيرة
- Extra Large: `16px` - للكروت والحاويات
- Full: `9999px` - للزوايا الدائرية الكاملة

### الظلال
- **Signature Shadow**: `0 4px 20px -2px rgba(0, 32, 69, 0.08)` 
  - ظل أساسي يستخدم لون navy مع شفافية منخفضة
  - يستخدم للكروت والعناصر المرفوعة

### الحدود
- **Default Border**: `1px solid #e2e8f0`
- **Dashed Border**: `1px dashed #c4c6cf` (للحقول الاختيارية)

## المكونات الرئيسية

### الأزرار
- **Primary Button**: Background: `#002045`, Text: White
- **Secondary Button**: Background: `#2c694e`, Text: White
- **Outline Button**: Border: `1.5px solid #e2e8f0`, Text: Dark
- **Active State**: Hover effect with elevated shadow

### البطاقات والكروت
- Background: White (`#ffffff`)
- Border: Light gray (`1px solid #e2e8f0`)
- Shadow: Institutional shadow (`0 4px 20px -2px rgba(0, 32, 69, 0.08)`)
- Border Radius: `16px`

### الشريط الجانبي
- **Background Gradient**: من Navy إلى Dark Navy
- **Text Color**: White
- **Active Item**: Light overlay (`rgba(255, 255, 255, .16)`)

### حقول الإدخال
- Border: `1px solid #e2e8f0`
- Focus: Border color changes to `#2c694e` (Secondary)
- Background: White
- Border Radius: `8px`

## الخلفيات والتأثيرات

### خلفية الصفحة
- **Color**: `#f7fafc` (Light Blue-Gray)
- **Pattern**: Radial gradient dots (`.1` opacity) - نمط دقيق للعمق
- **Pattern Size**: `24px x 24px`

### الانتقالات والحركات
- **Fade In**: `animation: fade .4s` - للصفحات الجديدة
- **Pop**: `animation: pop .5s` - للعناصر البارزة (مثل النجاح)

## معايير التصميم

### المسافات
- **Element Gap**: `2rem` (32px) - بين العناصر الرئيسية
- **Grid Gutter**: `1.5rem` (24px) - بين أعمدة الشبكة
- **Card Padding**: `2rem` (32px) - الحشوة الداخلية للبطاقات
- **Section Gap**: `4rem` (64px) - بين الأقسام الرئيسية

### المحاذاة
- **RTL (Right-to-Left)**: جميع النماذج مصممة للغة العربية
- **Max Width Container**: `1280px` للمحتوى الرئيسي

## ملاحظات مهمة

1. **الاحترافية**: استخدام navy وأخضر غابي يعكس الموثوقية والنمو
2. **الوضوح**: ظلال مكتومة بدلاً من الأسود الصرف لتحسين سهولة القراءة
3. **الانسجام**: جميع الألوان مستمدة من نظام Material Design 3
4. **الابتكار**: نمط الدقاط الدقيق يضيف عمقاً دون الزحام البصري
5. **إمكانية الوصول**: تباين عالي بين النصوص والخلفيات

---
تم التحديث: 29 يونيو 2026
نسخة: 1.0
