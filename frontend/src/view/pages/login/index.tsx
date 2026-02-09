import { useAuth } from "@/app/hooks/useAuth";
import { authService } from "@/app/services/auth";
import type { ILoginParams } from "@/app/services/auth/login";
import { Button } from "@/view/components/ui/button";
import {
  Card,
  CardContent,
  CardDescription,
  CardFooter,
  CardHeader,
  CardTitle,
} from "@/view/components/ui/card";
import { Input } from "@/view/components/ui/input";
import { Label } from "@/view/components/ui/label";
import { zodResolver } from "@hookform/resolvers/zod";
import { useMutation } from "@tanstack/react-query";
import { useForm } from "react-hook-form";
import toast from "react-hot-toast";
import { Link, useNavigate } from "react-router-dom";
import z from "zod/v4";

const loginSchema = z.object({
  email: z.email(),
  password: z.string().min(8),
});

type LoginFormData = z.infer<typeof loginSchema>;

export const Login = () => {
  const navigate = useNavigate();
  const { register, handleSubmit: hookFormSubmit } = useForm<LoginFormData>({
    resolver: zodResolver(loginSchema),
  });

  const { mutateAsync } = useMutation({
    mutationFn: async (data: ILoginParams) => {
      return authService.login(data);
    },
  });

  const { signin } = useAuth();

  const handleSubmit = hookFormSubmit(async (data) => {
    try {
      const { accessToken } = await mutateAsync(data);

      signin(accessToken);
    } catch (error) {
      toast.error("Credenciais inválidas!");
    }
  });

  return (
    <form
      className="w-full flex h-screen items-center justify-center border"
      onSubmit={handleSubmit}
    >
      <Card className="w-96">
        <CardHeader>
          <CardTitle>Entre com a sua conta</CardTitle>
          <CardDescription>
            Rapidamente você poderá criar a sua biblioteca virtual
          </CardDescription>
        </CardHeader>

        <CardContent className="space-y-4">
          <div className="space-y-2">
            <Label htmlFor="email">E-mail</Label>
            <Input
              id="email"
              type="email"
              placeholder="seu@email.com"
              {...register("email")}
            />
          </div>

          <div className="space-y-2">
            <Label htmlFor="password">Senha</Label>
            <Input
              id="password"
              type="password"
              placeholder="••••••••"
              {...register("password")}
            />
          </div>
        </CardContent>

        <CardFooter className="flex flex-col gap-4">
          <Button className="w-full">Entrar</Button>
          <p className="text-sm text-muted-foreground">
            Nao possui uma conta?{" "}
            <Link to="/" className="font-medium text-primary hover:underline">
              Cadastrar
            </Link>
          </p>
        </CardFooter>
      </Card>
    </form>
  );
};
