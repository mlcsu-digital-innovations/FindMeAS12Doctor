import { AuthService } from 'src/app/services/auth/auth.service';
import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-login',
  templateUrl: './login.page.html',
  styleUrls: ['./login.page.scss'],
})
export class LoginPage implements OnInit {
  public email: string;
  public password: string;

  constructor(
    private authService: AuthService
    ) { }

  ngOnInit() {
   
  }

  public login(): void {
    //this.authService.signIn();
    this.authService.login(this.email, this.password).subscribe(data => {
      console.log(data);
    }, error => {
      console.log(error);
    }); 
  }  
  
}
