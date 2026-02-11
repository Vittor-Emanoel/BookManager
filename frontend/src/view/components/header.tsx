import { BookOpen, PlusIcon } from "lucide-react";
import { Button } from "./ui/button";

export function Header() {
  return (
    <header className="flex items-center justify-between">
      <div className="flex items-center gap-2">
        <BookOpen className="size-8 text-zinc-900" />
        <h1 className="text-base text-zinc-800 leading-6">Minha Biblioteca</h1>
      </div>

      <Button>
        <PlusIcon />
        Adicionar livro
      </Button>
    </header>
  );
}
