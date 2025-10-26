import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class StorageService {
  private readonly CITY_KEY = 'ahhright_city';

  constructor() { }

  saveCity(city: string): void {
    localStorage.setItem(this.CITY_KEY, city);
  }

  getCity(): string | null {
    return localStorage.getItem(this.CITY_KEY);
  }

  clearCity(): void {
    localStorage.removeItem(this.CITY_KEY);
  }
}
