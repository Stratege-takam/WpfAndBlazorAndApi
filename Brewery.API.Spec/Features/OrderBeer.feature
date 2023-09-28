Feature: Order Beer
	This feature allow you to manage beer orders

Scenario: 1- Successfully add the sale of a beer to a wholesaler
	Given The user fills in the requested information (<clientId>, <wholesalerId>, <beers>) 
	When User submit the form wholesaler
	Then The beer is Sold with status code success
	And  An invoice is returned
	
Examples: 
  |clientId                             |wholesalerId                         |beers                                                                                                   |    
  |08d9c1d4-bd3b-4129-8153-4924870c9cb7 |08d9c2d4-bd3b-4129-8153-4924870c9cb1 | [{"Name": "Leffe Blonde"; "Count": 2};{"Name": "Beer 2"; "Count": 5};{"Name": "Beer 3"; "Count": 1}] |
  |08d9c1d4-bd3b-4129-8153-4924870c9cb7 |08d9c2d4-bd3b-4129-8153-4924870c9cb1 | [{"Name": "02d9c7d4-bd3b-4229-8153-4924870c3cb6"; "Count": 3};{"Name": "03d9c7d4-bd3b-4229-8153-4924870c3cb6"; "Count": 10}] |



Scenario: 2- Error try to add the sale of a beer to a wholesaler with empty order
	Given The user fills incorrectly requested information (<clientId>, <wholesalerId>) 
	When He submit the form wholesaler
	Then The server answer to status code BadParams
	And The reason is Order cannot be empty

Examples: 
  |clientId                             |wholesalerId                         |                                        
  |08d9c1d4-bd3b-4129-8153-4924870c9cb7 |08d9c2d4-bd3b-4129-8153-4924870c9cb1 | 
  |08d9c4d4-bd3b-4129-8153-4924870c9cb9 |08d9c2d4-bd3b-4129-8153-4924870c9cb2 | 
  |08d9c1d4-bd3b-4129-8153-4924870c9cb7 |08d9c2d4-bd3b-4129-8153-4924870c9cb3 | 



Scenario: 3- Error try to add duplicate order the sale of a beer to a wholesaler
	Given The user fills in the requested information with duplicate order (<clientId>, <wholesalerId>, <beers>) 
	When He submit the form wholesaler with duplicate order
	Then The server return to status code BadParams
	And The reason There cannot be a duplicate in the order

Examples: 
  |clientId                             |wholesalerId                         |beers                                                                                                                          |    
  |08d9c1d4-bd3b-4129-8153-4924870c9cb7 |08d9c2d4-bd3b-4129-8153-4924870c9cb1 | [{"Name": "Leffe Blonde"; "Count": 2};{"Name": "Beer 2"; "Count": 5};{"Name": "Beer 2"; "Count": 2};{"Name": "Beer 3"; "Count": 13}]                         |
  |08d9c1d4-bd3b-4129-8153-4924870c9cb7 |08d9c2d4-bd3b-4129-8153-4924870c9cb1 | [{"Name": "02d9c7d4-bd3b-4229-8153-4924870c3cb6"; "Count": 3};{"Name": "03d9c7d4-bd3b-4229-8153-4924870c3cb6"; "Count": 103};{"Name": "02d9c7d4-bd3b-4229-8153-4924870c3cb6"; "Count": 1}] |


Scenario: 4- Error try to add  the sale of a beer not exist to a wholesaler
	Given The user fills in the requested information with not exist beer (<clientId>, <wholesalerId>, <beers>) 
	When He submit the form wholesaler with not exist beer
	Then The server return to status code BadParams because the beer does't exist
	And The reason is The beer (xxx) must be sold by the wholesaler
	
Examples: 
  |clientId                             |wholesalerId                         |beers                                                                                                                          |    
  |08d9c1d4-bd3b-4129-8153-4924870c9cb7 |08d9c2d4-bd3b-4129-8153-4924870c9cb1 | [{"Name": "Leffe Blondes"; "Count": 2};{"Name": "Beer 22"; "Count": 5};{"Name": "Beer 33"; "Count": 1}]                          |
  |08d9c1d4-bd3b-4129-8153-4924870c9cb7 |08d9c2d4-bd3b-4129-8153-4924870c9cb1 | [{"Name": "02d9c7d4-bd3b-4029-8153-4924870c3cb6"; "Count": 3};{"Name": "03d9c0d4-bd3b-4229-8153-4924870c3cb6"; "Count": 103}] |


Scenario: 5- Error try to add  the sale of a beer not exist stock to a wholesaler
	Given The user fills in the requested information with not exist stock beer (<clientId>, <wholesalerId>, <beers>) 
	When He submit the form wholesaler with not exist stock beer
	Then The server return to status code BadParams because the beer does't exist stock
	And The reason is The number of beers (xxx) ordered must not exceed the wholesaler's stock
	

Examples: 
  |clientId                             |wholesalerId                         |beers                                                                                                                          |    
  |08d9c1d4-bd3b-4129-8153-4924870c9cb7 |08d9c2d4-bd3b-4129-8153-4924870c9cb1 | [{"Name": "Leffe Blonde"; "Count": 230};{"Name": "Beer 2"; "Count": 500};{"Name": "Beer 3"; "Count": 1000}]                          |
  |08d9c1d4-bd3b-4129-8153-4924870c9cb7 |08d9c2d4-bd3b-4129-8153-4924870c9cb1 | [{"Name": "02d9c7d4-bd3b-4229-8153-4924870c3cb6"; "Count": 3120};{"Name": "03d9c7d4-bd3b-4229-8153-4924870c3cb6"; "Count": 1030}] |




Scenario: 6- Error try to add  the sale of a beer to a non exist wholesaler
	Given The user fills in the requested information with non exist wholesaler (<clientId>, <wholesalerId>, <beers>) 
	When He submit the form wholesaler with non exist wholesaler
	Then The server return to status code BadParams becuase non exist wholesaler
	And The reason is The wholesaler must exist


Examples: 
  |clientId                             |wholesalerId                         |beers                                                                                                                          |    
  |08d9c1d4-bd3b-4129-8153-4924870c9cb7 |08d9c2d4-bd3b-4129-8153-4924870c9ca1 | [{"Name": "Leffe Blonde"; "Count": 2};{"Name": "Beer 2"; "Count": 5};{"Name": "Beer 3"; "Count": 1}]                          |
  |08d9c1d4-bd3b-4129-8153-4924870c9cb7 |08d9c2d4-bd3b-4129-8153-4924870c9cd1 | [{"Name": "02d9c7d4-bd3b-4229-8153-4924870c3cb6"; "Count": 3};{"Name": "03d9c7d4-bd3b-4229-8153-4924870c3cb6"; "Count": 103}] |



Scenario: 7- Error try to add  the sale of a beer to a wholesaler with non exist client
	Given The user fills in the requested information with non exist client (<clientId>, <wholesalerId>, <beers>) 
	When He submit the form wholesaler with non exist client
	Then The server return to status code BadParams becuase non exist client
	And The reason is The client must exist

Examples: 
  |clientId                             |wholesalerId                         |beers                                                                                                                          |    
  |08d9c1d4-bd3b-4129-8153-4924870c9ab7 |08d9c2d4-bd3b-4129-8153-4924870c9cb1 | [{"Name": "Leffe Blonde"; "Count": 2};{"Name": "Beer 2"; "Count": 1};{"Name": "Beer 3"; "Count": 1}]                          |
  |08d9c1d4-bd3b-4129-8153-4924870c9db7 |08d9c2d4-bd3b-4129-8153-4924870c9cb1 | [{"Name": "02d9c7d4-bd3b-4229-8153-4924870c3cb6"; "Count": 1};{"Name": "03d9c7d4-bd3b-4229-8153-4924870c3cb6"; "Count": 1}] |
