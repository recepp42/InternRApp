import { Role } from "../enums/role";

export interface User {
  id: number;
  email: string;
  role: Role;
}