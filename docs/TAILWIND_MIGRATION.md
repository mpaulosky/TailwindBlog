# Tailwind CSS Migration

This document outlines the migration from Bootstrap to Tailwind CSS v4 in the TailwindBlog application.

## Migration Summary

The application has been successfully migrated from Bootstrap to Tailwind CSS using version 4.1.13 (latest stable).

### Changes Made

1. **Removed Bootstrap Dependencies**
   - Removed Bootstrap CSS reference from `App.razor`
   - Deleted Bootstrap files from `wwwroot/lib/bootstrap/`

2. **Installed Tailwind CSS**
   - Added `tailwindcss@latest` and `@tailwindcss/postcss` packages
   - Added `postcss-cli` and `autoprefixer` for build pipeline
   - Created `tailwind.config.js` with proper content paths
   - Created `postcss.config.js` for PostCSS configuration

3. **Updated Layout Components**
   - **MainLayout.razor**: Converted to Flexbox layout using Tailwind classes
   - **NavMenu.razor**: Completely rewritten using Tailwind utility classes
   - **MainLayout.razor.css**: Emptied (replaced with Tailwind utilities)
   - **NavMenu.razor.css**: Emptied (replaced with Tailwind utilities)

4. **Updated Page Components**
   - **Counter.razor**: Converted button from Bootstrap `btn btn-primary` to Tailwind classes

5. **Created Build Pipeline**
   - Input file: `wwwroot/tailwind.css` (contains Tailwind directives)
   - Output file: `wwwroot/app.css` (generated CSS)
   - Build command: `npm run build:css`
   - Watch command: `npm run watch:css`

### Key Design Decisions

1. **Responsive Design**: Used Tailwind's responsive prefixes (`lg:`) to maintain mobile-first design
2. **Color Scheme**: Maintained the original blue-to-purple gradient for the sidebar
3. **Navigation Icons**: Kept the original SVG icons as inline background images
4. **Mobile Menu**: Implemented using CSS checkbox technique with Tailwind classes

### Build Commands

To rebuild the CSS after making changes:

```bash
cd src/Web
npm run build:css
```

To watch for changes during development:

```bash
cd src/Web
npm run watch:css
```

### File Structure

```
src/Web/
├── package.json                 # Contains build scripts
├── tailwind.config.js           # Tailwind configuration
├── postcss.config.js           # PostCSS configuration
├── wwwroot/
│   ├── tailwind.css            # Source CSS with Tailwind directives
│   └── app.css                 # Generated CSS (auto-generated)
└── Components/
    ├── App.razor               # Updated to reference app.css only
    ├── Layout/
    │   ├── MainLayout.razor    # Converted to Tailwind classes
    │   ├── MainLayout.razor.css # Emptied
    │   ├── NavMenu.razor       # Converted to Tailwind classes
    │   └── NavMenu.razor.css   # Emptied
    └── Pages/
        └── Counter.razor       # Button converted to Tailwind
```

### Preserved Functionality

- Responsive sidebar collapse on mobile
- Navigation highlighting for active pages
- Authentication menu states
- Error boundary styling
- Form validation styles

All original functionality has been preserved while gaining the benefits of Tailwind CSS utility-first approach.