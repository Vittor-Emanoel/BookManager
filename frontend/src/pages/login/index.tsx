import { Button } from "@/components/ui/button";
import {
  Card,
  CardContent,
  CardDescription,
  CardFooter,
  CardHeader,
  CardTitle,
} from "@/components/ui/card";
import { Input } from "@/components/ui/input";
import { Label } from "@/components/ui/label";

export const Login = () => {
  return (
    <div className="w-full flex h-screen items-center justify-center border">
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
            <Input id="email" type="email" placeholder="seu@email.com" />
          </div>

          <div className="space-y-2">
            <Label htmlFor="password">Senha</Label>
            <Input id="password" type="password" placeholder="••••••••" />
          </div>
        </CardContent>

        <CardFooter>
          <Button className="w-full">Criar conta</Button>
        </CardFooter>
      </Card>
    </div>
  );
};
