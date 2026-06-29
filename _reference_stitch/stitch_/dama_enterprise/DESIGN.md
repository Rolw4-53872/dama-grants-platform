---
name: DAMA Institutional
colors:
  surface: '#ffffff'
  surface-dim: '#d7dadc'
  surface-bright: '#f7fafc'
  surface-container-lowest: '#ffffff'
  surface-container-low: '#f1f4f6'
  surface-container: '#ebeef0'
  surface-container-high: '#e5e9eb'
  surface-container-highest: '#e0e3e5'
  on-surface: '#181c1e'
  on-surface-variant: '#44474e'
  inverse-surface: '#2d3133'
  inverse-on-surface: '#eef1f3'
  outline: '#e2e8f0'
  outline-variant: '#c4c6cf'
  surface-tint: '#465f87'
  primary: '#00091b'
  on-primary: '#ffffff'
  primary-container: '#002045'
  on-primary-container: '#7089b3'
  inverse-primary: '#aec7f5'
  secondary: '#2c694e'
  on-secondary: '#ffffff'
  secondary-container: '#aeeecb'
  on-secondary-container: '#316e52'
  tertiary: '#00081a'
  on-tertiary: '#ffffff'
  tertiary-container: '#002044'
  on-tertiary-container: '#6589c1'
  error: '#ba1a1a'
  on-error: '#ffffff'
  error-container: '#ffdad6'
  on-error-container: '#93000a'
  primary-fixed: '#d6e3ff'
  primary-fixed-dim: '#aec7f5'
  on-primary-fixed: '#001b3c'
  on-primary-fixed-variant: '#2e476e'
  secondary-fixed: '#b1f0ce'
  secondary-fixed-dim: '#95d4b3'
  on-secondary-fixed: '#002114'
  on-secondary-fixed-variant: '#0e5138'
  tertiary-fixed: '#d5e3ff'
  tertiary-fixed-dim: '#a7c8ff'
  on-tertiary-fixed: '#001b3c'
  on-tertiary-fixed-variant: '#1f477b'
  background: '#f7fafc'
  on-background: '#181c1e'
  surface-variant: '#e0e3e5'
  accent-line: '#2c694e'
  glass-overlay: rgba(0, 32, 69, 0.05)
typography:
  display-lg:
    fontFamily: Cairo
    fontSize: 128px
    fontWeight: '900'
    lineHeight: '1'
    letterSpacing: 0.2em
  display-lg-mobile:
    fontFamily: Cairo
    fontSize: 72px
    fontWeight: '900'
    lineHeight: '1'
    letterSpacing: 0.1em
  headline-xl:
    fontFamily: Cairo
    fontSize: 36px
    fontWeight: '700'
    lineHeight: '1.2'
  headline-lg:
    fontFamily: Cairo
    fontSize: 24px
    fontWeight: '700'
    lineHeight: '1.4'
  title-lg:
    fontFamily: Noto Sans Arabic
    fontSize: 20px
    fontWeight: '700'
    lineHeight: '1.5'
  body-lg:
    fontFamily: Noto Sans Arabic
    fontSize: 18px
    fontWeight: '500'
    lineHeight: '1.75'
  body-md:
    fontFamily: Noto Sans Arabic
    fontSize: 16px
    fontWeight: '400'
    lineHeight: '1.6'
  label-sm:
    fontFamily: Noto Sans Arabic
    fontSize: 14px
    fontWeight: '600'
    lineHeight: '1.2'
rounded:
  sm: 0.25rem
  DEFAULT: 0.5rem
  md: 0.75rem
  lg: 1rem
  xl: 1.5rem
  full: 9999px
spacing:
  container-max: 1280px
  section-gap: 4rem
  element-gap: 2rem
  card-padding: 2rem
  grid-gutter: 1.5rem
---

## Brand & Style
The brand identity is **Institutional Modernism**. It evokes a sense of prestige, academic excellence, and administrative reliability. The style is tailored for high-level governmental or academic entities, emphasizing transparency and structural integrity.

The visual language combines **Corporate Modern** elements—clean grids and professional typography—with **Subtle Geometry**. The use of radial gradients, cube-pattern overlays, and dashed circular motifs adds a layer of digital sophistication without compromising the formal nature of the "Grant and Qualification Management System." The emotional response should be one of trust, order, and technological advancement in the non-profit and educational sectors.

## Colors
The palette is rooted in a deep **Imperial Navy (#002045)**, representing authority and depth, paired with a **Forest Green (#2c694e)** that symbolizes growth, sustainability, and the non-profit "green" sector. 

- **Primary:** Used for high-impact brand elements, headers, and primary action containers.
- **Secondary:** Used for accentuation, status indicators, and subtle decorative lines to break the density of the navy.
- **Backgrounds:** A tiered system starting with a cool-toned light gray (`#f7fafc`) for the page body, moving to pure white (`#ffffff`) for elevated cards.
- **Subtle Gradients:** Backgrounds use very large, low-opacity radial blurs (5% opacity) of the primary and secondary colors to create a sense of depth and atmospheric lighting.

## Typography
The system uses a dual-font approach optimized for Arabic and Latin scripts. **Cairo** is reserved for high-impact headlines and the "DAMA" display text due to its geometric and architectural feel. **Noto Sans Arabic** handles all functional and body copy, ensuring maximum readability across varying pixel densities.

**Key Principles:**
- **Display Text:** The "DAMA" logo-type uses massive scaling (9xl) with extreme letter spacing to create a monumental presence.
- **Hierarchy:** Clear distinction through weight (900 for display, 700 for headers, 400-500 for body).
- **RTL Alignment:** Explicit focus on right-to-left flow, with secondary English subtitles or dates utilizing a lighter opacity to maintain primary focus on the Arabic content.

## Layout & Spacing
The layout follows a **Fixed-Width Centered Grid** approach for desktop, ensuring the institutional content feels grounded and contained.

- **Grid:** A standard 12-column system is implied, though the footer uses a simple 1 or 2 column split for project details.
- **Rhythm:** Generous vertical spacing (`section-gap`) is used to separate the header, branding, and footer "cards" to prevent visual clutter.
- **Safe Zones:** Containers use a maximum width of 1280px with 24px (1.5rem) side margins on mobile to ensure content doesn't hit the screen edges.
- **Responsive Behavior:** On mobile, the 2-column footer cards stack vertically, and the header logo-pair shifts from a horizontal row to a centered stack.

## Elevation & Depth
Elevation is achieved through a combination of **Tonal Layers** and **Ambient Shadows**, rather than heavy skeuomorphism.

- **Surfaces:** The primary background is the lowest layer. Cards and containers sit on top with a pure white surface.
- **Shadows:** A signature "Institutional Shadow" is used: `0 4px 20px -2px rgba(0, 32, 69, 0.08)`. This uses a navy-tinted shadow rather than pure black, making the elevation feel more integrated with the brand colors.
- **Textural Depth:** A low-opacity (3%) cube pattern is overlaid on the entire background to provide a tactile "paper-like" or "blueprint" feel to the digital space.

## Shapes
The shape language is **Structured and Professional**. 

- **Containers:** Standard cards use a `0.5rem` (8px) radius. Larger elements like the main primary-color feature card may use slightly larger radii (`lg` or 12px) to feel more substantial.
- **Decorative Elements:** The "Accent Line" is a simple 4px high rectangle with 2px rounded corners, used to underscore section titles.
- **Icons:** Icons are housed in rounded-xl (16px) square containers to give them a "stamp" or "badge" appearance.
- **Circular Motifs:** Used strictly as background decorative "technical drawings" (dashed circles) to suggest precision and engineering.

## Components
### Buttons & Actions
- **Primary Action:** Solid Navy (`#002045`) with white text, 8px radius, and high-weight Noto Sans Arabic text.
- **Secondary Action:** Outlined with a 1px border of `#e2e8f0`, or subtle green accents.

### Cards
- **Informational Card:** White background, 1px border (`#e2e8f0`), and the signature navy-tinted ambient shadow.
- **Feature Card:** High-contrast solid navy background with internal white borders (10% opacity) for structural divisions.

### Input Fields (Implicit)
- Should follow the card style: White background, 8px radius, 1px `#e2e8f0` border, focusing to the Primary Navy or Secondary Green.

### Icons
- Use **Material Symbols Outlined** with a weight of 400. In containers, icons should be sized at 36px-40px for visibility.

### Status Indicators
- Small 8px circles (pill-shaped) in Secondary Green used as list bullets to indicate "active" or "completed" items in a team or feature list.