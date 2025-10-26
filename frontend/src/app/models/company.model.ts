export interface CompanySearchRequest {
  city: string;
  businessName: string;
}

export interface CompanySearchResponse {
  companyName: string;
  companyNumber: string;
  previousNames: string[];
}
