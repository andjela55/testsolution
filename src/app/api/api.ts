export * from './health.service';
import { HealthService } from './health.service';
export * from './login.service';
import { LoginService } from './login.service';
export * from './register.service';
import { RegisterService } from './register.service';
export * from './user.service';
import { UserService } from './user.service';
export const APIS = [HealthService, LoginService, RegisterService, UserService];
