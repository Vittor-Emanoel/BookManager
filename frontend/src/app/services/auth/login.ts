import { httpClient } from "../httpClient"

export interface ILoginParams {
  email: string
  password: string
}

export interface ILoginResponse {
  accessToken: string
}

export async function login(params: ILoginParams) {
  const { data } = await httpClient.post<ILoginResponse>('auth/login', params)

  return data
}