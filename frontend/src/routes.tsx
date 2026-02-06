import { createBrowserRouter } from "react-router-dom";
import { Register } from "./pages/register";

export const router = createBrowserRouter([
  {
    path: "/",
    element: <Register />,
  },
]);
