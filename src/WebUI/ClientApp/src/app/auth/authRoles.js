export const authRoles = {
  sa: ['SA'], // Only Super Admin has access
  admin: ['SA', 'user'], // Only SA & Admin has access
  editor: ['SA', 'user', 'EDITOR'], // Only SA & Admin & Editor has access
  guest: ['SA', 'user', 'EDITOR', 'GUEST'] // Everyone has access
}

// Check out app/views/dashboard/DashboardRoutes.js
// Only SA & Admin has dashboard access

// const dashboardRoutes = [
//   {
//     path: "/dashboard/analytics",
//     component: Analytics,
//     auth: authRoles.admin <----------------
//   }
// ];