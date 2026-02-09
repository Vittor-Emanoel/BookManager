import { httpClient } from "../httpClient"

export interface IRegisterParams {
  name: string
  email: string
  password: string
}

export interface IRegisterResponse {
  userId: string
}


export async function register(params: IRegisterParams) {
  const { data } = await httpClient.post<IRegisterResponse>('auth/register', params)

  return data
}