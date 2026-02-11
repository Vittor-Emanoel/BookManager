import {
  Select,
  SelectContent,
  SelectGroup,
  SelectItem,
  SelectTrigger,
  SelectValue,
} from "@/components/ui/select";
import { zodResolver } from "@hookform/resolvers/zod";
import { Search } from "lucide-react";
import { Controller, useForm } from "react-hook-form";
import { useSearchParams } from "react-router-dom";
import z from "zod";

const searchFormSchema = z.object({
  query: z.string(),
  status: z.string(),
});

export function SearchBar() {
  const [_, setSearchParams] = useSearchParams();

  const { handleSubmit, register, control } = useForm({
    resolver: zodResolver(searchFormSchema),
  });

  const handleSearchText = handleSubmit((params) => {
    setSearchParams((state) => {
      if (params.query) {
        state.set("query", params.query);
      } else {
        state.delete("query");
      }

      return state;
    });

    setSearchParams((state) => {
      if (params.status) {
        state.set("bookStatus", params.status);
      } else {
        state.delete("bookStatus");
      }

      return state;
    });
  });

  return (
    <form className="flex items-center gap-4" onSubmit={handleSearchText}>
      <div className="bg-zinc-100 text-zinc-500 placeholder:text-zinc-500 flex items-center py-2 px-3 gap-3 rounded-lg flex-1 focus-within:border-zinc-600 focus-within:border border border-zinc-300 shadow">
        <Search className="size-4 text-zinc-400" />
        <input
          type="text"
          className="bg-transparent outline-none w-full"
          placeholder="Buscar por título ou autor..."
          {...register("query")}
        />
      </div>

      <Controller
        control={control}
        name="status"
        defaultValue="0"
        render={({ field: { onChange, value } }) => (
          <Select value={value} onValueChange={onChange}>
            <SelectTrigger className="w-45 bg-zinc-100 border border-zinc-300 py-5 shadow ring-0">
              <SelectValue
                placeholder="Theme"
                className="text-zinc-800 placeholder:text-zinc-900 "
              />
            </SelectTrigger>
            <SelectContent className="">
              <SelectGroup>
                <SelectItem value="0">Todos os status</SelectItem>
                <SelectItem value="1">Querendo ler</SelectItem>
                <SelectItem value="2">Lendo</SelectItem>
                <SelectItem value="3">Lido</SelectItem>
              </SelectGroup>
            </SelectContent>
          </Select>
        )}
      />
    </form>
  );
}
