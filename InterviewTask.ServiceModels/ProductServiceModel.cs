using System;

namespace InterviewTask.ServiceModels
{
    /// <summary>
    /// This Model in current scenario is not nessesary, as the business logic for CRUD functionality 
    /// is very simple, but if the solution will contain specific business logic(like calculate the average price of sold products
    /// , or generate custom report ), 
    /// then every business logic module will need its specific model.
    /// </summary>
    public class ProductServiceItemDetailsModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public bool IsAvailable { get; set; }
        public decimal Price { get; set; }
    }
}
