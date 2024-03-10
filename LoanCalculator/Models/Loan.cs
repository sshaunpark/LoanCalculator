using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LoanCalculator.Models
{
    public class Loan
    {
        [Required]
        [StringLength(8, ErrorMessage = "Grade can't be longer than 8 characters.")]
        public string Grade { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime IssueDate { get; set; }

        [Required]
        [Range(1, Int32.MaxValue, ErrorMessage = "Term must be a positive number.")]
        public int Term { get; set; }

        [Required]
        [Range(0, 100, ErrorMessage = "Coupon rate must be between 0 and 100 as a percentage value.")]
        public decimal CouponRate { get; set; }

        [Required]
        [Range(0.01, Double.MaxValue, ErrorMessage = "Invested amount must be greater than zero.")]
        public decimal Invested { get; set; }

        [Required]
        [Range(0, Double.MaxValue, ErrorMessage = "Outstanding balance cannot be negative.")]
        public decimal OutstandingBalance { get; set; }

        [Required]
        [Range(0, 100, ErrorMessage = "Recovery rate must be between 0 and 100 as a percentage value.")]
        public decimal RecoveryRate { get; set; }

        [Required]
        [Range(0, 100, ErrorMessage = "Purchase premium must be between 0 and 100 as a percentage value.")]
        public decimal PurchasePremium { get; set; }

        [Required]
        [Range(0, 100, ErrorMessage = "Servicing fee must be between 0 and 100 as a percentage value.")]
        public decimal ServicingFee { get; set; }

        [Required]
        [Range(0, 100, ErrorMessage = "Earnout fee must be between 0 and 100 as a percentage value.")]
        public decimal EarnoutFee { get; set; }

        public Loan(
            string grade,
            DateTime issueDate,
            int term,
            decimal couponRate,
            decimal invested,
            decimal outstandingBalance,
            decimal recoveryRate,
            decimal purchasePremium,
            decimal servicingFee,
            decimal earnoutFee)
        {
            Grade = grade;
            IssueDate = issueDate;
            Term = term;
            CouponRate = couponRate;
            Invested = invested;
            OutstandingBalance = outstandingBalance;
            RecoveryRate = recoveryRate;
            PurchasePremium = purchasePremium;
            ServicingFee = servicingFee;
            EarnoutFee = earnoutFee;

            Validate();
        }

        private void Validate()
        {
            var context = new ValidationContext(this);
            var results = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(this, context, results, true);

            if (!isValid)
            {
                var errorMessage = results.Aggregate(new StringBuilder("Loan terms are invalid:"), 
                    (sb, validationResult) => sb.AppendLine($" - {validationResult.ErrorMessage}"), sb => sb.ToString());

                throw new ValidationException(errorMessage);
            }
        }
    }
}