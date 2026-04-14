namespace PokemonReviewApp.Models
{
    public class Reviewer
    {
        public int Id { get; set; }

        // string တွေအတွက် string.Empty သုံးပါ
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        // Collection တွေအတွက် List အသစ်တစ်ခု ဆောက်ပေးထားပါ
        public ICollection<Review> Reviews { get; set; } = new List<Review>();

    }
} 
