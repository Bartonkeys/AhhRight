export interface CompanySearchRequest {
  city: string;
  businessName: string;
}

export interface CompanySearchResponse {
  companyName: string;
  companyNumber: string;
  previousNames: string[];
}

export interface StartupSearchRequest {
  incorporatedFrom?: string;
  incorporatedTo?: string;
  location?: string;
  sicCodes?: string[];
  companyStatus?: string;
  size?: number;
  startIndex?: number;
}

export interface StartupSearchResponse {
  companyName: string;
  companyNumber: string;
  companyStatus: string;
  dateOfCreation?: string;
  location?: string;
  sicCodes: string[];
  companyType?: string;
}

export interface StartupFeedResponse {
  totalResults: number;
  pageSize: number;
  currentPage: number;
  companies: StartupSearchResponse[];
}

export interface TrendAnalysisRequest {
  startDate: string;
  endDate: string;
  location?: string;
  sicCodes?: string[];
  groupBy?: string;
}

export interface TrendDataPoint {
  date: string;
  count: number;
  location?: string;
  sicCode?: string;
}

export interface TrendAnalysisResponse {
  startDate: string;
  endDate: string;
  totalCompanies: number;
  trendData: TrendDataPoint[];
  locationBreakdown: { [key: string]: number };
  industryBreakdown: { [key: string]: number };
}

export interface SicCode {
  code: string;
  description: string;
}
