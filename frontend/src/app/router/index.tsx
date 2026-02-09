import { Dashboard } from "@/view/pages/dashboard";
import { Login } from "@/view/pages/login";
import { Register } from "@/view/pages/register";
import { BrowserRouter, Route, Routes } from "react-router-dom";
import { AuthGuard } from "./authGuard";

export function Router() {
  return (
    <BrowserRouter>
      <Routes>
        <Route element={<AuthGuard isPrivate={false} />}>
          <Route path="/login" element={<Login />} />
          <Route path="/register" element={<Register />} />
        </Route>

        <Route element={<AuthGuard isPrivate />}>
          <Route path="/dashboard" element={<Dashboard />} />
        </Route>
      </Routes>
    </BrowserRouter>
  );
}
