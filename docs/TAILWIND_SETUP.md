# TailwindCSS Setup

This project has been migrated from Bootstrap to TailwindCSS v4.

## Prerequisites

- Node.js and npm (for TailwindCSS build process)
- .NET 9 SDK

## Development Workflow

### Building CSS

To build the TailwindCSS:

```bash
npm run build:css:prod
```

For development with watch mode:

```bash
npm run build:css
```

### Project Structure

- `src/Web/wwwroot/app-tailwind.css` - Source TailwindCSS file with custom components
- `src/Web/wwwroot/dist/app.css` - Compiled TailwindCSS output (included in the application)
- `package.json` - npm configuration with TailwindCSS CLI dependency

### Key Changes

1. **Removed Bootstrap**: All Bootstrap references have been removed from `App.razor`
2. **Added TailwindCSS v4**: Using the latest alpha version with @import "tailwindcss"
3. **Custom Components**: Created reusable CSS classes like `btn-primary`, `nav-link`, etc.
4. **Responsive Design**: Updated layouts to use TailwindCSS responsive utilities
5. **Icon Integration**: Converted Bootstrap Icons to inline SVG with mask for better integration

### CSS Architecture

The TailwindCSS setup includes:

- **Base layer**: Global styles and font configuration
- **Components layer**: Reusable component styles (buttons, forms, navigation)
- **Utilities layer**: Custom utility classes like `sidebar-gradient`

### Mobile Navigation

The mobile navigation uses the same checkbox approach as before but with TailwindCSS classes and improved responsive behavior.

## Running the Application

```bash
# Restore packages
dotnet restore

# Build CSS (if not already built)
npm run build:css:prod

# Run the application
dotnet run --project src/Web
```

## Notes

- The compiled CSS is included in source control for deployment simplicity
- Source CSS file should be edited in `app-tailwind.css`, not the compiled output
- Bootstrap references have been completely removed
- All custom styling now uses TailwindCSS utility classes or custom components