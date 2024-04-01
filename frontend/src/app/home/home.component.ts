import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-home',
  standalone: true,
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss'], // Add your CSS file here
  imports: [FormsModule]
})
export class HomeComponent {
  userInput: string = ''; // User input for sending
  receivedData: string = ''; // Data received via websocket
  sendStatus: string = 'Idle'; // Initial status for sending
  receiveStatus: string = 'Listening'; // Initial status for receiving

  sendData() {
    // Simulate sending data to backend (replace with actual API call)
    this.sendStatus = 'Typing';
    // ... Send data to backend via API (e.g., ListenerService)

    // Simulate receiving data from backend (replace with actual websocket handling)
    setTimeout(() => {
      this.receivedData = 'Data from backend'; // Replace with actual received data
      this.receiveStatus = 'Refreshed';
    }, 2000); // Simulating delay
  }
}