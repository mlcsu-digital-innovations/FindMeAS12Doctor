import { AuthService } from 'src/app/services/auth/auth.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-login',
  templateUrl: './login.page.html',
  styleUrls: ['./login.page.scss'],
})
export class LoginPage implements OnInit {
 
  constructor(
    private authService: AuthService
    ) { }

  ngOnInit() {
   
  }

  public login(): void {
    this.authService.signIn();
  }  

  public logoff(): void {
    this.authService.signOut();    
  }
}
