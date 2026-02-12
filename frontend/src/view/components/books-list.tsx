import { useBooks } from "@/app/hooks/useBooks";
import { useSearchParams } from "react-router-dom";

export function BooksList() {
  const [searchParams] = useSearchParams();

  const query = searchParams.get("query");
  const bookStatus = searchParams.get("bookStatus");

  const { books } = useBooks({ query, bookStatus });

  return <div>{books?.map((book) => <p>{book.name}</p>)}</div>;
}
