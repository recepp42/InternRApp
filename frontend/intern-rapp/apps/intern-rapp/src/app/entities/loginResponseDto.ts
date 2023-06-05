import { Role } from '../enums/role';
import { User } from './user';

export interface LoginResponseDto {
  access_token: string;
  user: User;
}
