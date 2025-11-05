import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { 
  CompanySearchRequest, 
  CompanySearchResponse,
  StartupSearchRequest,
  StartupFeedResponse,
  TrendAnalysisRequest,
  TrendAnalysisResponse
} from '../models/company.model';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private apiUrl = environment.apiUrl;

  constructor(private http: HttpClient) { }

  searchCompanies(request: CompanySearchRequest): Observable<CompanySearchResponse[]> {
    return this.http.post<CompanySearchResponse[]>(`${this.apiUrl}/api/company/search`, request);
  }

  getStartupFeed(request: StartupSearchRequest): Observable<StartupFeedResponse> {
    return this.http.post<StartupFeedResponse>(`${this.apiUrl}/api/company/startups/feed`, request);
  }

  getDailyStartups(location?: string, sicCodes?: string, size: number = 50): Observable<StartupFeedResponse> {
    let url = `${this.apiUrl}/api/company/startups/daily?size=${size}`;
    if (location) {
      url += `&location=${encodeURIComponent(location)}`;
    }
    if (sicCodes) {
      url += `&sicCodes=${encodeURIComponent(sicCodes)}`;
    }
    return this.http.get<StartupFeedResponse>(url);
  }

  getStartupTrends(request: TrendAnalysisRequest): Observable<TrendAnalysisResponse> {
    return this.http.post<TrendAnalysisResponse>(`${this.apiUrl}/api/company/startups/trends`, request);
  }

  getSicCodes(): Observable<{ [key: string]: string }> {
    return this.http.get<{ [key: string]: string }>(`${this.apiUrl}/api/company/sic-codes`);
  }
}
