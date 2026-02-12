import type { Books } from "@/app/entities/Book";
import { httpClient } from "../httpClient";

interface ISearchParams {
  query: string | null
  bookStatus: string | null
}

interface ISearchResponse{
  data: Books[]
}

export async function search(params:ISearchParams) {
  const { data } =  await httpClient.get<ISearchResponse>('books/search', {
    params
  })

  return data.data
}