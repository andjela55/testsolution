export class AppRoutes {
  public static readonly Login = 'login';
  get Login(): string {
    return this.Login;
  }
  public static readonly Chat = 'chat';
  get Chat(): string {
    return this.Chat;
  }
  public static readonly Main = '';
  get Main(): string {
    return this.Chat;
  }
}
