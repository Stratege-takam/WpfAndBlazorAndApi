Feature: Beer
	Brewery beer management


Scenario: 1- Successful creation of a new beer
	Given User fills in the name of the beer "Leffe black"
	And User enters the identifier of the beer brewer "08d9c2d4-bd3b-4129-8153-4924870c9cb7"
	And User enters the price of the beer 12.39
	And User enters the degree of the beer 34.88
	When User submit result
	Then The status code is Success
	And  The degree must be "34,88%"
	And The identifiant must be not null


Scenario: 2- Error creating a beer with a non-existent brewer
	Given The user fills in the requested information with a non-existent brewer (<name>,<ownerId>,<price>,<degree>)
	When User submit the form
	Then The status code is BadParams
	And  The reason is The identification of the introduced owner does not exist
	And The data is null
	
Examples: 
  | name        | ownerId                              | price | degree |
  | Beer test 1 | 08d9c2d4-bd3b-4120-8153-4924870c9cb7 | 11.23 | 21.10  |
  | Beer test 2 | 08d9c2d4-bd3b-4100-8153-4924870c9cb7 | 13.0 | 1.00  |
  | Beer test 3 | 08d9c2d4-bd3b-4000-8153-4924870c9cb7 | 100 | 12.00  |
  | Beer test 4 | 08d9c2d4-bd3b-4002-8153-4924870c9cb7 | 33.11 | 2.25  |
  | Beer test 5 | 08d9c2d4-bd3b-2002-8153-4924870c9cb7 | 12.12 | 21.15  |

  
Scenario: 3- Successful creation of many beer
	Given The user fills in the all requested information (<name>,<ownerId>,<price>,<degree>)
	When The user submits his request and the system processes the information
	Then The all status code is Success
	And  The system returns each registered beer to the user with ID

Examples: 
  | name        | ownerId                              | price | degree |
  | Beer test 1 | 08d9c2d4-bd3b-4129-8153-4924870c9cb7 | 11.23 | 21.10  |
  | Beer test 2 | 08d9c2d4-bd3b-4129-8153-4924870c9cb7 | 13.0  | 1.00   |
  | Beer test 3 | 08d9c2d4-bd3b-4129-8153-4924870c9cb7 | 100   | 12.00  |
  | Beer test 4 | 08d9c2d4-bd3b-4129-8153-4924870c9cb8 | 33.11 | 2.25   |
  | Beer test 5 | 08d9c2d4-bd3b-4129-8153-4924870c9cb8 | 12.12 | 21.15  |



@mytag
Scenario: 4- Error when creating an existing beer
	Given The user fills the form (<name>,<ownerId>,<price>,<degree>)
	When He submit
	Then Every status code is BadParams
	And  The reason is A beer already exists under this name. Beer name must be unique
	And The data is null because an existing beer

Examples: 
  | name         | ownerId                              | price | degree |
  | Leffe Blonde | 08d9c2d4-bd3b-4129-8153-4924870c9cb7 | 11.23 | 21.10  |
  | Beer 2       | 08d9c2d4-bd3b-4129-8153-4924870c9cb7 | 13.0  | 1.00   |
  | Beer 3       | 08d9c2d4-bd3b-4129-8153-4924870c9cb7 | 100   | 12.00  |
  | Beer 4       | 08d9c2d4-bd3b-4129-8153-4924870c9cb8 | 33.11 | 2.25   |
  | Beer 5       | 08d9c2d4-bd3b-4129-8153-4924870c9cb8 | 12.12 | 21.15  |

