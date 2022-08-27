import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ShowMessagesComponent } from './home/forum/show-topics/show-messages/show-messages.component';
import { HomeComponent } from './home/home.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { ManageUsersComponent } from './manage-users/manage-users.component';
import { UserMessagesComponent } from './manage-users/user-messages/user-messages.component';
import { UserProfileComponent } from './manage-users/user-profile/user-profile.component';

const routes: Routes = [
  {path: '', component: HomeComponent},
  {path: 'login', component: LoginComponent},
  {path: 'register', component: RegisterComponent},
  {path: 'manage-users', component: ManageUsersComponent },
  {path: 'view/:topicId',  component: ShowMessagesComponent},
  {path: 'manage-users/messages/:userId',  component: UserMessagesComponent},
  {path: 'view/topicId/view/userId',  component: UserProfileComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
