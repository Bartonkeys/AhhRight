import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import {
  IonHeader,
  IonToolbar,
  IonTitle,
  IonContent,
  IonSearchbar,
  IonList,
  IonItem,
  IonLabel,
  IonCheckbox,
  IonButton,
  IonButtons,
  IonIcon,
  IonBadge,
  ModalController
} from '@ionic/angular/standalone';
import { ApiService } from '../services/api.service';
import { SicCode } from '../models/company.model';

@Component({
  selector: 'app-sic-code-selector-modal',
  templateUrl: './sic-code-selector-modal.component.html',
  styleUrls: ['./sic-code-selector-modal.component.scss'],
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    IonHeader,
    IonToolbar,
    IonTitle,
    IonContent,
    IonSearchbar,
    IonList,
    IonItem,
    IonLabel,
    IonCheckbox,
    IonButton,
    IonButtons,
    IonIcon,
    IonBadge
  ]
})
export class SicCodeSelectorModalComponent implements OnInit {
  allSicCodes: SicCode[] = [];
  filteredSicCodes: SicCode[] = [];
  selectedSicCodes: Set<string> = new Set();
  searchTerm: string = '';
  loading: boolean = false;

  constructor(
    private modalController: ModalController,
    private apiService: ApiService
  ) {}

  ngOnInit() {
    this.loadSicCodes();
  }

  loadSicCodes() {
    this.loading = true;
    this.apiService.getSicCodes().subscribe({
      next: (data) => {
        this.allSicCodes = Object.entries(data).map(([code, description]) => ({
          code,
          description
        }));
        this.filteredSicCodes = [...this.allSicCodes];
        this.loading = false;
      },
      error: (err) => {
        console.error('Error loading SIC codes:', err);
        this.loading = false;
      }
    });
  }

  filterSicCodes(event: any) {
    const searchTerm = event.target.value?.toLowerCase() || '';
    this.searchTerm = searchTerm;

    if (!searchTerm) {
      this.filteredSicCodes = [...this.allSicCodes];
      return;
    }

    this.filteredSicCodes = this.allSicCodes.filter(sicCode =>
      sicCode.code.toLowerCase().includes(searchTerm) ||
      sicCode.description.toLowerCase().includes(searchTerm)
    );
  }

  toggleSicCode(code: string) {
    if (this.selectedSicCodes.has(code)) {
      this.selectedSicCodes.delete(code);
    } else {
      this.selectedSicCodes.add(code);
    }
  }

  isSicCodeSelected(code: string): boolean {
    return this.selectedSicCodes.has(code);
  }

  clearSelection() {
    this.selectedSicCodes.clear();
  }

  dismiss() {
    this.modalController.dismiss();
  }

  confirm() {
    const selectedCodes = Array.from(this.selectedSicCodes);
    this.modalController.dismiss(selectedCodes);
  }
}
