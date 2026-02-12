export enum BookStatus {
  ToRead = 1,
  Reading = 2,
  Read = 3
}

export interface Books {
  id: string
  userId: string
  name: string
  author: string
  imageUrl?: string
  rating: number
  status: BookStatus
  description?: string
}
