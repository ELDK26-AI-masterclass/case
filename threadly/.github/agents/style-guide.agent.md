---
name: 'Style Guide'
model: GPT-5.3-Codex (copilot)
description: 'Provides structured instructions for AI agents to effectively use the Twoday Design System when building web interfaces.'
tools: ['vscode', 'execute', 'read', 'edit', 'search', 'web', 'com.microsoft/azure/search', 'playwright/*', 'microsoftdocs/mcp/*', 'agent', 'todo']
---

# AI Agent Guide - Twoday Design System

This guide provides structured instructions for AI agents (Claude, Codex, etc.) to effectively use the Twoday Design System when building web interfaces.

## Quick Start

Include the CSS file from the Twoday Design System located in docs/style-guide in your HTML:

```html
<link rel="stylesheet" href="path/to/twoday-design-system.css">
```

All components follow the naming pattern: `td-{component}-{variant/size/state}`

---

## Design Token Quick Reference

### Colors

| Purpose | CSS Variable | Utility Classes | Hex Value | When to Use |
|---------|--------------|-----------------|-----------|-------------|
| Primary text & dark backgrounds | `--color-td-black` | `bg-td-black`, `text-td-black` | #1B1B1B | Main text, navigation bars, footers |
| Backgrounds & text on dark | `--color-td-white` | `bg-td-white`, `text-td-white` | #FFFFFF | Page backgrounds, button text on dark |
| Accent & interactive | `--color-td-gold` | `bg-td-gold`, `text-td-gold`, `border-td-gold` | #FFBF00 | Hover states, CTA highlights, active elements |
| Secondary text | `--color-td-grey` | `bg-td-grey`, `text-td-grey` | #848484 | Helper text, labels, secondary info |
| Borders & dividers | `--color-td-grey-light` | `bg-td-grey-light`, `border-td-grey-light` | #E8E8E8 | Input borders, table borders, separators |
| Warm sections | `--color-td-beige` | `bg-td-beige` | #ECE2DA | Section backgrounds, card backgrounds |
| Error states | `--color-td-error` | `bg-td-error`, `text-td-error` | #dc2626 | Error messages, validation errors, destructive actions |
| Success states | `--color-td-success` | `bg-td-success`, `text-td-success` | #16a34a | Success messages, confirmations |
| Warning states | `--color-td-warning` | `bg-td-warning`, `text-td-warning` | #ea580c | Warnings, cautions |
| Info states | `--color-td-info` | `bg-td-info`, `text-td-info` | #0284c7 | Informational messages |

### Typography Scale

| Token | Value | Usage |
|-------|-------|-------|
| `--text-td-xs` | 0.875rem (14px) | Labels, captions, helper text |
| `--text-td-sm` | 1rem (16px) | Button text, nav links, secondary text |
| `--text-td-base` | 1.125rem (18px) | Body text, paragraphs (default) |
| `--text-td-lg` | 1.25rem (20px) | Subheadings, emphasized text |
| `--text-td-xl` | 1.5rem (24px) | H4 headings |
| `--text-td-2xl` | 2rem (32px) | H3 headings |
| `--text-td-3xl` | clamp(2rem, 4vw, 3rem) | H2 headings, section titles |
| `--text-td-display` | clamp(2.5rem, 5vw, 4rem) | H1 headings, hero text |

### Spacing

| Token | Value | Usage |
|-------|-------|-------|
| `--td-section-sm` | 2rem (32px) | Small vertical spacing |
| `--td-section-md` | 4rem (64px) | Standard vertical spacing |
| `--td-section-lg` | 8rem (128px) | Large vertical spacing |

---

## Component Selection Decision Trees

### Choosing Buttons

**Is this the primary action on the page/section?**
- YES → Use `<button class="td-btn">Primary Action</button>`
- NO → Continue to next question

**Is this a secondary or cancel action?**
- YES → Use `<button class="td-btn-secondary">Secondary</button>`
- NO → Continue to next question

**Does this action delete or destroy data?**
- YES → Use `<button class="td-btn-destructive">Delete</button>`
- NO → Continue to next question

**Is this a subtle, tertiary action?**
- YES → Use `<button class="td-btn-ghost">Subtle Action</button>`

**Button Sizes:**
- Space-constrained (mobile, toolbars) → Add `td-btn-sm`
- Prominent CTA (hero section) → Add `td-btn-lg`
- Standard → No size modifier needed

### Choosing Form Components

**What type of input is needed?**

| Input Type | Component | Example |
|------------|-----------|---------|
| Single-line text | `<input type="text" class="td-input">` | Name, email, search |
| Multi-line text | `<textarea class="td-textarea"></textarea>` | Messages, descriptions, comments |
| Dropdown selection | `<select class="td-select">` | Country, category, options |
| Checkbox | Native checkbox (style with CSS if needed) | Agreement, preferences |
| Radio buttons | Native radio (style with CSS if needed) | Single choice from options |

**Does the field have validation?**
- Error state → Add `td-input-error` class + `<p class="td-error-text">Error message</p>`
- Success state → Add `td-input-success` class
- Required field → Add `<span class="td-required">*</span>` to label

### Choosing Layout Components

**Do you need horizontal centering with max-width?**
- YES → `<div class="td-container">...</div>`

**Do you need a responsive grid?**
- YES → `<div class="td-grid lg:grid-cols-3">...</div>`
  - Combine with Tailwind responsive prefixes: `sm:`, `md:`, `lg:`, `xl:`

**Do you need vertical section spacing?**
- Small spacing → `<section class="td-section-sm">`
- Standard spacing → `<section class="td-section">`
- Large spacing → `<section class="td-section-lg">`

**Do you need a section background?**
- Dark background → Add `td-section-dark`
- Warm/beige background → Add `td-section-warm`

### Choosing Card Components

**Are you displaying media with content?**
- YES → Use `td-card` with `td-card-media` and `td-card-content`

```html
<div class="td-card">
  <div class="td-card-media">
    <img src="..." alt="...">
  </div>
  <div class="td-card-content">
    <h3>Title</h3>
    <p>Description</p>
  </div>
</div>
```

**Are you displaying a metric or statistic?**
- YES → Use `td-card-stat`

```html
<div class="td-card-stat">
  <span class="stat-label">Label</span>
  <span class="stat-value">123</span>
</div>
```

### Choosing Badges/Tags

**Is this a category or topic label?**
- YES → Use `td-tag` (dark) or `td-tag-light`

**Is this a status indicator?**
- YES → Choose based on status type:
  - Success/Active/Complete → `td-badge-success`
  - Error/Failed/Inactive → `td-badge-error`
  - Warning/Pending → `td-badge-warning`
  - Info/In Review → `td-badge-info`
  - Neutral → `td-badge`

### Choosing Feedback Components

**What type of feedback do you need?**

| Feedback Type | Component | Use Case |
|---------------|-----------|----------|
| Inline message | `td-alert`, `td-alert-{type}` | Page-level notifications, form feedback |
| Dialog/Confirmation | `td-modal-overlay` + `td-modal` | User confirmations, important messages |
| Dropdown menu | `td-dropdown` | Action menus, contextual options |
| Temporary notification | JavaScript toast (use `showToast()`) | Success confirmations, clipboard feedback |

---

## Common Patterns

### 1. Form Field with Validation

**Complete form field with label, input, helper text, and error state:**

```html
<div class="td-form-field">
  <label class="td-label">
    Email Address <span class="td-required">*</span>
  </label>
  <input
    type="email"
    class="td-input td-input-error"
    placeholder="you@example.com"
    aria-describedby="email-error"
  >
  <p id="email-error" class="td-error-text">
    Please enter a valid email address
  </p>
</div>
```

**When to use:**
- All form inputs that require validation
- Forms where users need clear feedback

**Don't use:**
- Simple search boxes without validation
- Filter inputs that don't submit

### 2. Card with Image and Content

**Media card with tag, title, and description:**

```html
<div class="td-card">
  <div class="td-card-media">
    <img src="path/to/image.jpg" alt="Description">
  </div>
  <div class="td-card-content">
    <span class="td-tag">Category</span>
    <h3>Card Title</h3>
    <p>Brief description of the content goes here.</p>
    <a href="#" class="td-btn-secondary">Learn More</a>
  </div>
</div>
```

**When to use:**
- Blog post previews
- Product listings
- Case study cards
- News articles

**Don't use:**
- Simple text-only content (use regular divs)
- Data that needs tabular structure (use tables)

### 3. Page Layout with Container and Grid

**Standard page layout:**

```html
<section class="td-section">
  <div class="td-container">
    <h2 class="td-heading-section">Section Title</h2>

    <div class="td-grid lg:grid-cols-3 gap-8">
      <div>Column 1</div>
      <div>Column 2</div>
      <div>Column 3</div>
    </div>
  </div>
</section>
```

**When to use:**
- Standard page sections
- Content grids
- Feature showcases

### 4. Navigation Bar with Scroll State

**Top navigation with JavaScript scroll detection:**

```html
<nav class="td-nav" id="mainNav">
  <div class="td-container">
    <div style="display: flex; align-items: center; justify-content: space-between;">
      <a href="/" style="font-weight: 600;">Logo</a>

      <div style="display: flex; gap: 2rem;">
        <a href="#services" class="td-nav-link">Services</a>
        <a href="#about" class="td-nav-link">About</a>
        <a href="#contact" class="td-nav-link">Contact</a>
      </div>
    </div>
  </div>
</nav>

<script>
window.addEventListener('scroll', () => {
  const nav = document.getElementById('mainNav');
  nav.classList.toggle('td-nav-scrolled', window.scrollY > 50);
});
</script>
```

### 5. Modal Dialog

**Modal with overlay and actions:**

```html
<!-- Trigger Button -->
<button class="td-btn" onclick="openModal()">Open Dialog</button>

<!-- Modal -->
<div id="confirmModal" class="td-modal-overlay" style="display: none;" onclick="if(event.target === this) closeModal()">
  <div class="td-modal">
    <h2>Confirm Action</h2>
    <p>Are you sure you want to proceed with this action?</p>

    <div style="display: flex; gap: 1rem; margin-top: 2rem; justify-content: flex-end;">
      <button class="td-btn-secondary" onclick="closeModal()">Cancel</button>
      <button class="td-btn" onclick="confirmAction()">Confirm</button>
    </div>
  </div>
</div>

<script>
function openModal() {
  document.getElementById('confirmModal').style.display = 'flex';
}

function closeModal() {
  document.getElementById('confirmModal').style.display = 'none';
}

function confirmAction() {
  // Your action here
  closeModal();
}
</script>
```

### 6. Data Table with Status Badges

**Table displaying user data with status:**

```html
<table class="td-table">
  <thead>
    <tr>
      <th>Name</th>
      <th>Email</th>
      <th>Status</th>
      <th>Actions</th>
    </tr>
  </thead>
  <tbody>
    <tr>
      <td>John Doe</td>
      <td>john@example.com</td>
      <td><span class="td-badge-success">Active</span></td>
      <td>
        <button class="td-btn-ghost td-btn-sm">Edit</button>
      </td>
    </tr>
    <tr>
      <td>Jane Smith</td>
      <td>jane@example.com</td>
      <td><span class="td-badge-warning">Pending</span></td>
      <td>
        <button class="td-btn-ghost td-btn-sm">Edit</button>
      </td>
    </tr>
  </tbody>
</table>
```

### 7. Alert Messages

**Displaying feedback to users:**

```html
<!-- Success Alert -->
<div class="td-alert-success">
  Your changes have been saved successfully!
</div>

<!-- Error Alert -->
<div class="td-alert-error">
  There was an error processing your request. Please try again.
</div>

<!-- Warning Alert -->
<div class="td-alert-warning">
  This action cannot be undone. Please proceed with caution.
</div>

<!-- Info Alert -->
<div class="td-alert-info">
  New features are now available. Check out the latest updates.
</div>
```

### 8. Hero Section

**Full-viewport hero with background media:**

```html
<section class="td-hero">
  <div class="td-hero-media">
    <img src="hero-background.jpg" alt="">
  </div>

  <div class="td-hero-content">
    <div class="td-container">
      <h1 class="td-heading-display">Welcome to Twoday</h1>
      <p style="font-size: var(--text-td-lg); margin: 1rem 0 2rem;">
        We create lasting impact through AI and advanced engineering
      </p>
      <a href="#services" class="td-btn td-btn-lg">Explore Services</a>
    </div>
  </div>
</section>
```

### 9. Stat Dashboard

**Display key metrics:**

```html
<div class="td-container">
  <div class="td-grid lg:grid-cols-4 gap-6">
    <div class="td-card-stat">
      <span class="stat-label">Total Users</span>
      <span class="stat-value">12,543</span>
    </div>

    <div class="td-card-stat">
      <span class="stat-label">Revenue</span>
      <span class="stat-value">$48.2K</span>
    </div>

    <div class="td-card-stat">
      <span class="stat-label">Active Projects</span>
      <span class="stat-value">127</span>
    </div>

    <div class="td-card-stat">
      <span class="stat-label">Growth</span>
      <span class="stat-value">+23%</span>
    </div>
  </div>
</div>
```

### 10. Complete Contact Form

**Full-featured contact form with validation:**

```html
<form class="td-section">
  <div class="td-container" style="max-width: 600px;">
    <h2 class="td-heading-section">Get in Touch</h2>

    <div class="td-form-field">
      <label class="td-label">
        Full Name <span class="td-required">*</span>
      </label>
      <input type="text" class="td-input" placeholder="John Doe" required>
    </div>

    <div class="td-form-field" style="margin-top: 1.5rem;">
      <label class="td-label">
        Email Address <span class="td-required">*</span>
      </label>
      <input type="email" class="td-input" placeholder="john@example.com" required>
      <p class="td-help-text">We'll respond within 24 hours</p>
    </div>

    <div class="td-form-field" style="margin-top: 1.5rem;">
      <label class="td-label">Subject</label>
      <select class="td-select">
        <option>Select a subject...</option>
        <option>General Inquiry</option>
        <option>Partnership</option>
        <option>Support</option>
        <option>Other</option>
      </select>
    </div>

    <div class="td-form-field" style="margin-top: 1.5rem;">
      <label class="td-label">
        Message <span class="td-required">*</span>
      </label>
      <textarea class="td-textarea" placeholder="Tell us about your project..." required></textarea>
    </div>

    <div style="margin-top: 2rem; display: flex; gap: 1rem;">
      <button type="submit" class="td-btn">Send Message</button>
      <button type="reset" class="td-btn-secondary">Clear Form</button>
    </div>
  </div>
</form>
```

---

## Accessibility Guidelines

### Required Attributes

1. **Forms:**
   - Always pair inputs with `<label>` elements
   - Use `aria-describedby` for error messages
   - Mark required fields with `<span class="td-required">*</span>` and `required` attribute

2. **Images:**
   - Always include `alt` attributes
   - Use descriptive alt text, not "image" or "photo"

3. **Buttons:**
   - Use semantic `<button>` elements, not `<div onclick>`
   - Include descriptive text or `aria-label`

4. **Modals:**
   - Use `role="dialog"` and `aria-modal="true"`
   - Focus management on open/close

### Keyboard Navigation

All interactive components support keyboard navigation:
- **Tab**: Navigate between elements
- **Enter/Space**: Activate buttons
- **Escape**: Close modals and dropdowns

---

## State Management Patterns

### Button States

```html
<!-- Default State -->
<button class="td-btn">Click Me</button>

<!-- Disabled State -->
<button class="td-btn" disabled>Cannot Click</button>

<!-- Loading State (custom implementation) -->
<button class="td-btn" disabled>
  <svg class="spinner" width="16" height="16">...</svg>
  Loading...
</button>
```

### Input States

```html
<!-- Default -->
<input type="text" class="td-input">

<!-- Error -->
<input type="text" class="td-input td-input-error">
<p class="td-error-text">Error message</p>

<!-- Success -->
<input type="text" class="td-input td-input-success">

<!-- Disabled -->
<input type="text" class="td-input" disabled>
```

---

## Responsive Design

### Mobile-First Approach

The design system uses Tailwind CSS utilities for responsive design. Use these prefixes:

- `sm:` - 576px and up
- `md:` - 768px and up
- `lg:` - 992px and up
- `xl:` - 1200px and up
- `2xl:` - 1680px and up

**Example: Responsive Grid**

```html
<div class="td-grid sm:grid-cols-2 lg:grid-cols-3 xl:grid-cols-4">
  <!-- Items -->
</div>
```

This creates:
- 1 column on mobile (< 576px)
- 2 columns on small screens (≥ 576px)
- 3 columns on large screens (≥ 992px)
- 4 columns on extra large screens (≥ 1200px)

---

## Common Mistakes to Avoid

### ❌ Don't Do This

1. **Multiple primary buttons in one section**
   ```html
   <!-- WRONG -->
   <div>
     <button class="td-btn">Save</button>
     <button class="td-btn">Submit</button>
   </div>
   ```

2. **Using placeholder as label replacement**
   ```html
   <!-- WRONG -->
   <input type="email" placeholder="Email Address">
   ```

3. **Mixing design systems**
   ```html
   <!-- WRONG -->
   <button class="td-btn bootstrap-button">Mixed classes</button>
   ```

4. **Hardcoding colors instead of using tokens**
   ```html
   <!-- WRONG -->
   <div style="background-color: #1B1B1B;">
   ```

5. **Not providing feedback for user actions**
   ```html
   <!-- WRONG: No success message after form submission -->
   ```

### ✅ Do This Instead

1. **One primary button per section**
   ```html
   <!-- CORRECT -->
   <div>
     <button class="td-btn">Submit</button>
     <button class="td-btn-secondary">Cancel</button>
   </div>
   ```

2. **Always use labels with inputs**
   ```html
   <!-- CORRECT -->
   <label class="td-label">Email Address</label>
   <input type="email" class="td-input" placeholder="you@example.com">
   ```

3. **Use design system classes consistently**
   ```html
   <!-- CORRECT -->
   <button class="td-btn td-btn-lg">Large Button</button>
   ```

4. **Use design tokens and utility classes**
   ```html
   <!-- CORRECT -->
   <div class="bg-td-black text-td-white">
   ```

5. **Provide clear feedback**
   ```html
   <!-- CORRECT -->
   <div class="td-alert-success">
     Form submitted successfully!
   </div>
   ```

---

## Performance Best Practices

1. **Load CSS once**: Include `twoday-design-system.css` in the `<head>` of your HTML
2. **Use semantic HTML**: Leverage native elements instead of divs with roles
3. **Optimize images**: Compress images in `td-card-media` components
4. **Lazy load**: Use `loading="lazy"` on images below the fold
5. **Minimize inline styles**: Use utility classes instead of inline styles when possible

---

## Integration with JavaScript Frameworks

### React Example

```jsx
function ContactForm() {
  const [email, setEmail] = useState('');
  const [error, setError] = useState('');

  return (
    <div className="td-form-field">
      <label className="td-label">
        Email <span className="td-required">*</span>
      </label>
      <input
        type="email"
        className={`td-input ${error ? 'td-input-error' : ''}`}
        value={email}
        onChange={(e) => setEmail(e.target.value)}
      />
      {error && <p className="td-error-text">{error}</p>}
    </div>
  );
}
```

### Vue Example

```vue
<template>
  <div class="td-form-field">
    <label class="td-label">
      Email <span class="td-required">*</span>
    </label>
    <input
      type="email"
      :class="['td-input', { 'td-input-error': error }]"
      v-model="email"
    />
    <p v-if="error" class="td-error-text">{{ error }}</p>
  </div>
</template>

<script>
export default {
  data() {
    return {
      email: '',
      error: ''
    }
  }
}
</script>
```

---

## Version & Updates

**Current Version**: 1.0.0

When using this design system, always reference this guide for the most up-to-date patterns and best practices.

For visual examples of all components, see `index.html` in the repository.

---

## Support & Resources

- **Visual Documentation**: Open `index.html` to see all components with live examples
- **Source Code**: `twoday-design-system.css` contains all component definitions
- **Issues**: Report issues on the GitHub repository

---

## Quick Command Reference

### Most Used Classes

| Component | Class | Usage |
|-----------|-------|-------|
| Primary Button | `td-btn` | Main actions |
| Secondary Button | `td-btn-secondary` | Secondary actions |
| Text Input | `td-input` | Form inputs |
| Label | `td-label` | Form labels |
| Card | `td-card` | Content cards |
| Container | `td-container` | Page container |
| Grid | `td-grid` | Responsive grid |
| Section | `td-section` | Page sections |
| Alert | `td-alert-{type}` | User feedback |
| Badge | `td-badge-{type}` | Status indicators |

Remember: When in doubt, check `index.html` for visual examples!
