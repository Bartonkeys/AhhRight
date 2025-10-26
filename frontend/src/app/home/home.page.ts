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
  IonText
} from '@ionic/angular/standalone';
import { ApiService } from '../services/api.service';
import { StorageService } from '../services/storage.service';
import { CompanySearchResponse } from '../models/company.model';

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
    IonText
  ],
})
export class HomePage implements OnInit {
  city: string = '';
  businessName: string = '';
  results: CompanySearchResponse[] = [];
  loading: boolean = false;
  error: string = '';
  showCityInput: boolean = false;

  constructor(
    private apiService: ApiService,
    private storageService: StorageService
  ) {}

  ngOnInit() {
    const savedCity = this.storageService.getCity();
    if (savedCity) {
      this.city = savedCity;
    } else {
      this.showCityInput = true;
    }
  }

  saveCity() {
    if (this.city.trim()) {
      this.storageService.saveCity(this.city.trim());
      this.showCityInput = false;
    }
  }

  changeCity() {
    this.showCityInput = true;
  }

  searchCompanies() {
    if (!this.businessName.trim()) {
      this.error = 'Please enter a business name';
      return;
    }

    if (!this.city.trim()) {
      this.error = 'Please enter a city';
      this.showCityInput = true;
      return;
    }

    this.loading = true;
    this.error = '';
    this.results = [];

    this.apiService.searchCompanies({
      city: this.city.trim(),
      businessName: this.businessName.trim()
    }).subscribe({
      next: (data) => {
        this.results = data;
        this.loading = false;
        if (data.length === 0) {
          this.error = 'No results found';
        }
      },
      error: (err) => {
        this.loading = false;
        this.error = 'Error searching companies. Please try again.';
        console.error('Error:', err);
      }
    });
  }
}
