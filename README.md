# AhhRight

AhhRight is an application that helps you find the previous names of businesses in the UK. When someone mentions a place and you knew it by a different name, AhhRight helps you discover what it was previously called.

## Features

- Search for businesses by name and city
- View previous company names from Companies House records
- City is stored locally for convenience
- UK-focused using the Companies House API

## Tech Stack

### Backend
- .NET 8.0 Web API
- Companies House API integration
- CORS enabled for frontend communication

### Frontend
- Angular with Ionic Framework
- Standalone components
- Local storage for city persistence
- Responsive design

## Prerequisites

- .NET 8.0 SDK
- Node.js (v18 or higher)
- npm
- Companies House API Key (get one at https://developer.company-information.service.gov.uk/)

## Setup Instructions

### Backend Setup

1. Navigate to the backend directory:
   ```bash
   cd backend
   ```

2. Update the `appsettings.json` file with your Companies House API key:
   ```json
   {
     "CompaniesHouse": {
       "ApiKey": "YOUR_API_KEY_HERE"
     }
   }
   ```

3. Run the backend:
   ```bash
   dotnet run
   ```

   The API will be available at `http://localhost:5000`

### Frontend Setup

1. Navigate to the frontend directory:
   ```bash
   cd frontend
   ```

2. Install dependencies:
   ```bash
   npm install
   ```

3. Update the environment file if needed (`src/environments/environment.ts`):
   ```typescript
   export const environment = {
     production: false,
     apiUrl: 'http://localhost:5000'
   };
   ```

4. Run the frontend:
   ```bash
   ionic serve
   ```

   The app will be available at `http://localhost:8100`

## Usage

1. On first launch, enter your city (e.g., "London", "Manchester")
2. The city will be saved locally for future searches
3. Enter a business name to search
4. View the current name and any previous names the business had
5. You can change your city at any time using the "Change City" button

## API Endpoints

### POST /api/company/search
Search for companies by name and city.

**Request Body:**
```json
{
  "city": "London",
  "businessName": "The Crown"
}
```

**Response:**
```json
[
  {
    "companyName": "The Crown Pub Ltd",
    "companyNumber": "12345678",
    "previousNames": [
      "The Old Crown (until 2020-01-15)",
      "Crown Tavern (until 2015-06-30)"
    ]
  }
]
```

## Project Structure

```
AhhRight/
├── backend/
│   ├── Controllers/
│   │   └── CompanyController.cs
│   ├── Models/
│   │   ├── CompanySearchRequest.cs
│   │   ├── CompanySearchResponse.cs
│   │   └── CompaniesHouseModels.cs
│   ├── Services/
│   │   ├── ICompaniesHouseService.cs
│   │   └── CompaniesHouseService.cs
│   ├── Program.cs
│   └── appsettings.json
└── frontend/
    └── src/
        └── app/
            ├── home/
            │   ├── home.page.ts
            │   ├── home.page.html
            │   └── home.page.scss
            ├── models/
            │   └── company.model.ts
            └── services/
                ├── api.service.ts
                └── storage.service.ts
```

## License

MIT
