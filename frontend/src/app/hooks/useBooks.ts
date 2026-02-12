import { useQuery } from "@tanstack/react-query"
import { ReactQueryKeys } from "../config/reactQueryKeys"
import { booksService } from "../services/books"

interface UseBooksParams {
  query: string | null
  bookStatus: string | null
}

export const useBooks = (params: UseBooksParams) => {
  const {data, isLoading} = useQuery({
    queryKey: [ReactQueryKeys.searchBooks, params],
    queryFn: async () => await booksService.search(params)
  })

  return {
    books: data,
    isLoadingBooks: isLoading 
  }
}