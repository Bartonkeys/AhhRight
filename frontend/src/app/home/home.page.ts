import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { 
  IonHeader, 
  IonToolbar, 
  IonTitle, 
  IonContent, 
  IonCard, 
  IonCardHeader, 
  IonCardTitle, 
  IonCardContent,
  IonItem,
  IonLabel,
  IonInput,
  IonButton,
  IonList,
  IonSpinner,
  IonText,
  IonBadge
} from '@ionic/angular/standalone';
import { ApiService } from '../services/api.service';
import { StartupSearchResponse } from '../models/company.model';

@Component({
  selector: 'app-home',
  templateUrl: 'home.page.html',
  styleUrls: ['home.page.scss'],
  imports: [
    CommonModule,
    FormsModule,
    IonHeader, 
    IonToolbar, 
    IonTitle, 
    IonContent, 
    IonCard, 
    IonCardHeader, 
    IonCardTitle, 
    IonCardContent,
    IonItem,
    IonLabel,
    IonInput,
    IonButton,
    IonList,
    IonSpinner,
    IonText,
    IonBadge
  ],
})
export class HomePage implements OnInit {
  location: string = '';
  sicCodes: string = '';
  dateFrom: string = '';
  dateTo: string = '';
  startups: StartupSearchResponse[] = [];
  totalResults: number = 0;
  loading: boolean = false;
  error: string = '';
  searched: boolean = false;

  constructor(
    private apiService: ApiService
  ) {}

  ngOnInit() {
    const today = new Date();
    const lastWeek = new Date();
    lastWeek.setDate(today.getDate() - 7);
    
    this.dateTo = this.formatDate(today);
    this.dateFrom = this.formatDate(lastWeek);
  }

  formatDate(date: Date): string {
    return date.toISOString().split('T')[0];
  }

  loadStartups() {
    this.loading = true;
    this.error = '';
    this.startups = [];
    this.searched = true;

    const sicCodesArray = this.sicCodes.trim() 
      ? this.sicCodes.split(',').map(code => code.trim()).filter(code => code)
      : undefined;

    this.apiService.getStartupFeed({
      incorporatedFrom: this.dateFrom || undefined,
      incorporatedTo: this.dateTo || undefined,
      location: this.location.trim() || undefined,
      sicCodes: sicCodesArray,
      companyStatus: 'active',
      size: 50
    }).subscribe({
      next: (data) => {
        this.startups = data.companies;
        this.totalResults = data.totalResults;
        this.loading = false;
        if (data.companies.length === 0) {
          this.error = 'No companies found matching your criteria';
        }
      },
      error: (err) => {
        this.loading = false;
        this.error = 'Error loading startups. Please try again.';
        console.error('Error:', err);
      }
    });
  }

  loadDailyStartups() {
    this.loading = true;
    this.error = '';
    this.startups = [];
    this.searched = true;

    const sicCodesString = this.sicCodes.trim() || undefined;

    this.apiService.getDailyStartups(
      this.location.trim() || undefined,
      sicCodesString,
      50
    ).subscribe({
      next: (data) => {
        this.startups = data.companies;
        this.totalResults = data.totalResults;
        this.loading = false;
        if (data.companies.length === 0) {
          this.error = 'No companies registered today matching your criteria';
        }
      },
      error: (err) => {
        this.loading = false;
        this.error = 'Error loading daily startups. Please try again.';
        console.error('Error:', err);
      }
    });
  }
}
