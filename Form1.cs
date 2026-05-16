using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace DB_P3
{
    public partial class Form1 : Form
    {
        string connectionString = "Server=localhost;Database=master;Trusted_Connection=True;";

        public Form1()
        {
            InitializeComponent();
        }

        // INSERT BOOK
        private void button1_Click(object sender, EventArgs e)
        {
            using (InsertBookForm inputForm = new InsertBookForm())
            {
                if (inputForm.ShowDialog() == DialogResult.OK)
                {
                    string query = "INSERT INTO Book(ISBN, Title, Genre, Target_age) VALUES(@ISBN, @Title, @Genre, @Age)";
                    try
                    {
                        using (SqlConnection conn = new SqlConnection(connectionString))
                        {
                            conn.Open();
                            using (SqlCommand cmd = new SqlCommand(query, conn))
                            {
                                cmd.Parameters.AddWithValue("@ISBN", inputForm.ISBN);
                                cmd.Parameters.AddWithValue("@Title", inputForm.Title);
                                cmd.Parameters.AddWithValue("@Genre", inputForm.Genre);
                                cmd.Parameters.AddWithValue("@Age", inputForm.TargetAge);
                                cmd.ExecuteNonQuery();
                            }
                        }
                        MessageBox.Show("Book inserted successfully!");
                    }
                    catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
                }
            }
        }

        // INSERT AUTHOR
        private void button3_Click(object sender, EventArgs e)
        {
            using (InsertAuthorForm inputForm = new InsertAuthorForm())
            {
                if (inputForm.ShowDialog() == DialogResult.OK)
                {
                    string query = "INSERT INTO Authors(author_id, first_name, last_name, biography, royalty_percentages) VALUES(@ID, @First, @Last, @Bio, @Royalty)";
                    try
                    {
                        using (SqlConnection conn = new SqlConnection(connectionString))
                        {
                            conn.Open();
                            using (SqlCommand cmd = new SqlCommand(query, conn))
                            {
                                cmd.Parameters.AddWithValue("@ID", inputForm.AuthorId);
                                cmd.Parameters.AddWithValue("@First", inputForm.FirstName);
                                cmd.Parameters.AddWithValue("@Last", inputForm.LastName);
                                cmd.Parameters.AddWithValue("@Bio", inputForm.Biography);
                                cmd.Parameters.AddWithValue("@Royalty", inputForm.RoyaltyPct);
                                cmd.ExecuteNonQuery();
                            }
                        }
                        MessageBox.Show("Author inserted successfully!");
                    }
                    catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
                }
            }
        }

        // DELETE BOOK
        private void btnDeleteBook_Click(object sender, EventArgs e)
        {
            string isbn = Microsoft.VisualBasic.Interaction.InputBox(
                "Enter the ISBN of the book to delete:", "Delete Book", "");
            if (string.IsNullOrWhiteSpace(isbn)) return;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("DELETE FROM Book WHERE ISBN = @ISBN", conn))
                    {
                        cmd.Parameters.AddWithValue("@ISBN", isbn);
                        int rows = cmd.ExecuteNonQuery();
                        MessageBox.Show(rows > 0 ? "Book deleted!" : "No book found with that ISBN.");
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
        }

        // DELETE ORDER
        private void btnDeleteOrder_Click(object sender, EventArgs e)
        {
            string orderId = Microsoft.VisualBasic.Interaction.InputBox(
                "Enter the Order ID to delete:", "Delete Order", "");
            if (string.IsNullOrWhiteSpace(orderId)) return;

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand("DELETE FROM [Order Details] WHERE order_id = @ID", conn))
                    {
                        cmd.Parameters.AddWithValue("@ID", orderId);
                        cmd.ExecuteNonQuery();
                    }
                    using (SqlCommand cmd = new SqlCommand("DELETE FROM [Order] WHERE order_id = @ID", conn))
                    {
                        cmd.Parameters.AddWithValue("@ID", orderId);
                        int rows = cmd.ExecuteNonQuery();
                        MessageBox.Show(rows > 0 ? "Order deleted!" : "No order found with that ID.");
                    }
                }
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
        }

        // UPDATE ORDER DETAILS
        private void btnUpdateOrderDetails_Click(object sender, EventArgs e)
        {
            using (UpdateOrderDetailsForm f = new UpdateOrderDetailsForm())
            {
                if (f.ShowDialog() == DialogResult.OK)
                {
                    string query = @"UPDATE [Order Details] 
                                     SET Quantity = @Qty, unit_price = @Price, subtotal = @Sub 
                                     WHERE order_detail_id = @ID";
                    try
                    {
                        using (SqlConnection conn = new SqlConnection(connectionString))
                        {
                            conn.Open();
                            using (SqlCommand cmd = new SqlCommand(query, conn))
                            {
                                cmd.Parameters.AddWithValue("@ID", f.OrderDetailId);
                                cmd.Parameters.AddWithValue("@Qty", f.Quantity);
                                cmd.Parameters.AddWithValue("@Price", f.UnitPrice);
                                cmd.Parameters.AddWithValue("@Sub", f.Subtotal);
                                int rows = cmd.ExecuteNonQuery();
                                MessageBox.Show(rows > 0 ? "Order Details updated!" : "ID not found.");
                            }
                        }
                    }
                    catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
                }
            }
        }

        // UPDATE FORMAT
        private void btnUpdateFormat_Click(object sender, EventArgs e)
        {
            using (UpdateFormatForm f = new UpdateFormatForm())
            {
                if (f.ShowDialog() == DialogResult.OK)
                {
                    string query = @"UPDATE Format 
                                     SET retails_price = @Price, Format_Type = @Type, production_cost = @Cost 
                                     WHERE format_id = @ID";
                    try
                    {
                        using (SqlConnection conn = new SqlConnection(connectionString))
                        {
                            conn.Open();
                            using (SqlCommand cmd = new SqlCommand(query, conn))
                            {
                                cmd.Parameters.AddWithValue("@ID", f.FormatId);
                                cmd.Parameters.AddWithValue("@Price", f.RetailPrice);
                                cmd.Parameters.AddWithValue("@Type", f.FormatType);
                                cmd.Parameters.AddWithValue("@Cost", f.ProductionCost);
                                int rows = cmd.ExecuteNonQuery();
                                MessageBox.Show(rows > 0 ? "Format updated!" : "ID not found.");
                            }
                        }
                    }
                    catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
                }
            }
        }

        // SELECT TABLE
        private void btnSelectTable_Click(object sender, EventArgs e)
        {
            // Ask user which table
            string table = Microsoft.VisualBasic.Interaction.InputBox(
                "Which table to view?\n(Book / Authors / Format / Order / Order Details / Royalty / Retail Partner)",
                "Select Table", "Book");
            if (string.IsNullOrWhiteSpace(table)) return;

            // Map friendly names to actual SQL table names
            string sqlTable;
            switch (table.Trim().ToLower())
            {
                case "book": sqlTable = "Book"; break;
                case "authors": sqlTable = "Authors"; break;
                case "format": sqlTable = "Format"; break;
                case "order": sqlTable = "[Order]"; break;
                case "order details": sqlTable = "[Order Details]"; break;
                case "royalty": sqlTable = "Royalty"; break;
                case "retail partner": sqlTable = "[Retail Partner]"; break;
                default:
                    MessageBox.Show("Table not recognised. Try: Book, Authors, Format, Order, Order Details, Royalty, Retail Partner");
                    return;
            }

            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM " + sqlTable, conn);
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
            }
            catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
        }

        // JOIN TABLES
        // CROSS JOIN TABLES
        private void btnJoinTables_Click(object sender, EventArgs e)
        {
            using (CrossJoinForm f = new CrossJoinForm())
            {
                if (f.ShowDialog() == DialogResult.OK)
                {
                    string query = $"SELECT * FROM {f.Table1} CROSS JOIN {f.Table2}";
                    try
                    {
                        using (SqlConnection conn = new SqlConnection(connectionString))
                        {
                            conn.Open();
                            SqlDataAdapter da = new SqlDataAdapter(query, conn);
                            DataTable dt = new DataTable();
                            da.Fill(dt);
                            dataGridView1.DataSource = dt;
                        }
                    }
                    catch (Exception ex) { MessageBox.Show("Error: " + ex.Message); }
                }
            }
        }

        // UNUSED button4
        private void button4_Click(object sender, EventArgs e) { }
    }

    // INPUT FORM: INSERT BOOK
    public class InsertBookForm : Form
    {
        public string ISBN { get; private set; }
        public string Title { get; private set; }
        public string Genre { get; private set; }
        public int TargetAge { get; private set; }

        private TextBox txtISBN = new TextBox(), txtTitle = new TextBox(),
                        txtGenre = new TextBox(), txtAge = new TextBox();

        public InsertBookForm()
        {
            this.Text = "Insert Book";
            this.Size = new System.Drawing.Size(320, 260);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;

            var layout = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 5, Padding = new System.Windows.Forms.Padding(10) };
            layout.Controls.Add(new Label { Text = "ISBN:", Anchor = AnchorStyles.Left }, 0, 0); layout.Controls.Add(txtISBN, 1, 0);
            layout.Controls.Add(new Label { Text = "Title:", Anchor = AnchorStyles.Left }, 0, 1); layout.Controls.Add(txtTitle, 1, 1);
            layout.Controls.Add(new Label { Text = "Genre:", Anchor = AnchorStyles.Left }, 0, 2); layout.Controls.Add(txtGenre, 1, 2);
            layout.Controls.Add(new Label { Text = "Target Age:", Anchor = AnchorStyles.Left }, 0, 3); layout.Controls.Add(txtAge, 1, 3);

            var btnOK = new Button { Text = "Insert", DialogResult = DialogResult.OK };
            btnOK.Click += (s, ev) =>
            {
                if (!int.TryParse(txtAge.Text, out int age)) { MessageBox.Show("Age must be a number."); return; }
                ISBN = txtISBN.Text; Title = txtTitle.Text; Genre = txtGenre.Text; TargetAge = age;
            };
            layout.Controls.Add(btnOK, 1, 4);
            this.Controls.Add(layout);
            this.AcceptButton = btnOK;
        }
    }

    // INPUT FORM: INSERT AUTHOR
    public class InsertAuthorForm : Form
    {
        public string AuthorId { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Biography { get; private set; }
        public double RoyaltyPct { get; private set; }

        private TextBox txtId = new TextBox(), txtFirst = new TextBox(),
                        txtLast = new TextBox(), txtBio = new TextBox(), txtRoyalty = new TextBox();

        public InsertAuthorForm()
        {
            this.Text = "Insert Author";
            this.Size = new System.Drawing.Size(320, 300);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;

            var layout = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 6, Padding = new System.Windows.Forms.Padding(10) };
            layout.Controls.Add(new Label { Text = "Author ID:", Anchor = AnchorStyles.Left }, 0, 0); layout.Controls.Add(txtId, 1, 0);
            layout.Controls.Add(new Label { Text = "First Name:", Anchor = AnchorStyles.Left }, 0, 1); layout.Controls.Add(txtFirst, 1, 1);
            layout.Controls.Add(new Label { Text = "Last Name:", Anchor = AnchorStyles.Left }, 0, 2); layout.Controls.Add(txtLast, 1, 2);
            layout.Controls.Add(new Label { Text = "Biography:", Anchor = AnchorStyles.Left }, 0, 3); layout.Controls.Add(txtBio, 1, 3);
            layout.Controls.Add(new Label { Text = "Royalty %:", Anchor = AnchorStyles.Left }, 0, 4); layout.Controls.Add(txtRoyalty, 1, 4);

            var btnOK = new Button { Text = "Insert", DialogResult = DialogResult.OK };
            btnOK.Click += (s, ev) =>
            {
                if (!double.TryParse(txtRoyalty.Text, out double r)) { MessageBox.Show("Royalty must be a number."); return; }
                AuthorId = txtId.Text; FirstName = txtFirst.Text; LastName = txtLast.Text;
                Biography = txtBio.Text; RoyaltyPct = r;
            };
            layout.Controls.Add(btnOK, 1, 5);
            this.Controls.Add(layout);
            this.AcceptButton = btnOK;
        }
    }

    //INPUT FORM: UPDATE ORDER DETAILS
    public class UpdateOrderDetailsForm : Form
    {
        public string OrderDetailId { get; private set; }
        public int Quantity { get; private set; }
        public double UnitPrice { get; private set; }
        public double Subtotal { get; private set; }

        private TextBox txtId = new TextBox(), txtQty = new TextBox(),
                        txtPrice = new TextBox(), txtSub = new TextBox();

        public UpdateOrderDetailsForm()
        {
            this.Text = "Update Order Details";
            this.Size = new System.Drawing.Size(320, 260);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;

            var layout = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 5, Padding = new System.Windows.Forms.Padding(10) };
            layout.Controls.Add(new Label { Text = "Order Detail ID:", Anchor = AnchorStyles.Left }, 0, 0); layout.Controls.Add(txtId, 1, 0);
            layout.Controls.Add(new Label { Text = "Quantity:", Anchor = AnchorStyles.Left }, 0, 1); layout.Controls.Add(txtQty, 1, 1);
            layout.Controls.Add(new Label { Text = "Unit Price:", Anchor = AnchorStyles.Left }, 0, 2); layout.Controls.Add(txtPrice, 1, 2);
            layout.Controls.Add(new Label { Text = "Subtotal:", Anchor = AnchorStyles.Left }, 0, 3); layout.Controls.Add(txtSub, 1, 3);

            var btnOK = new Button { Text = "Update", DialogResult = DialogResult.OK };
            btnOK.Click += (s, ev) =>
            {
                if (!int.TryParse(txtQty.Text, out int qty)) { MessageBox.Show("Quantity must be a number."); return; }
                if (!double.TryParse(txtPrice.Text, out double pr)) { MessageBox.Show("Unit Price must be a number."); return; }
                if (!double.TryParse(txtSub.Text, out double sub)) { MessageBox.Show("Subtotal must be a number."); return; }
                OrderDetailId = txtId.Text; Quantity = qty; UnitPrice = pr; Subtotal = sub;
            };
            layout.Controls.Add(btnOK, 1, 4);
            this.Controls.Add(layout);
            this.AcceptButton = btnOK;
        }
    }

    // INPUT FORM: UPDATE FORMAT
    public class UpdateFormatForm : Form
    {
        public string FormatId { get; private set; }
        public double RetailPrice { get; private set; }
        public string FormatType { get; private set; }
        public double ProductionCost { get; private set; }

        private TextBox txtId = new TextBox(), txtPrice = new TextBox(),
                        txtType = new TextBox(), txtCost = new TextBox();

        public UpdateFormatForm()
        {
            this.Text = "Update Format";
            this.Size = new System.Drawing.Size(320, 260);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;

            var layout = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 5, Padding = new System.Windows.Forms.Padding(10) };
            layout.Controls.Add(new Label { Text = "Format ID:", Anchor = AnchorStyles.Left }, 0, 0); layout.Controls.Add(txtId, 1, 0);
            layout.Controls.Add(new Label { Text = "Retail Price:", Anchor = AnchorStyles.Left }, 0, 1); layout.Controls.Add(txtPrice, 1, 1);
            layout.Controls.Add(new Label { Text = "Format Type:", Anchor = AnchorStyles.Left }, 0, 2); layout.Controls.Add(txtType, 1, 2);
            layout.Controls.Add(new Label { Text = "Production Cost:", Anchor = AnchorStyles.Left }, 0, 3); layout.Controls.Add(txtCost, 1, 3);

            var btnOK = new Button { Text = "Update", DialogResult = DialogResult.OK };
            btnOK.Click += (s, ev) =>
            {
                if (!double.TryParse(txtPrice.Text, out double pr)) { MessageBox.Show("Retail Price must be a number."); return; }
                if (!double.TryParse(txtCost.Text, out double cost)) { MessageBox.Show("Production Cost must be a number."); return; }
                FormatId = txtId.Text; RetailPrice = pr; FormatType = txtType.Text; ProductionCost = cost;
            };
            layout.Controls.Add(btnOK, 1, 4);
            this.Controls.Add(layout);
            this.AcceptButton = btnOK;
        }
    }
}
// INPUT FORM: CROSS JOIN
public class CrossJoinForm : Form
{
    public string Table1 { get; private set; }
    public string Table2 { get; private set; }

    private ComboBox cmbTable1 = new ComboBox();
    private ComboBox cmbTable2 = new ComboBox();

    public CrossJoinForm()
    {
        this.Text = "Cross Join Tables";
        this.Size = new System.Drawing.Size(320, 180);
        this.FormBorderStyle = FormBorderStyle.FixedDialog;
        this.StartPosition = FormStartPosition.CenterParent;

        string[] tables = { "Book", "Authors", "Format", "[Order]", "[Order Details]", "Royalty", "[Retail Partner]", "[written by]" };
        cmbTable1.Items.AddRange(tables); cmbTable1.DropDownStyle = ComboBoxStyle.DropDownList; cmbTable1.SelectedIndex = 0;
        cmbTable2.Items.AddRange(tables); cmbTable2.DropDownStyle = ComboBoxStyle.DropDownList; cmbTable2.SelectedIndex = 1;

        var layout = new TableLayoutPanel { Dock = DockStyle.Fill, ColumnCount = 2, RowCount = 3, Padding = new System.Windows.Forms.Padding(10) };
        layout.Controls.Add(new Label { Text = "Table 1:", Anchor = AnchorStyles.Left }, 0, 0); layout.Controls.Add(cmbTable1, 1, 0);
        layout.Controls.Add(new Label { Text = "Table 2:", Anchor = AnchorStyles.Left }, 0, 1); layout.Controls.Add(cmbTable2, 1, 1);

        var btnOK = new Button { Text = "Join", DialogResult = DialogResult.OK };
        btnOK.Click += (s, ev) =>
        {
            if (cmbTable1.SelectedItem.ToString() == cmbTable2.SelectedItem.ToString())
            {
                MessageBox.Show("Please select two different tables.");
                return;
            }
            Table1 = cmbTable1.SelectedItem.ToString();
            Table2 = cmbTable2.SelectedItem.ToString();
        };

        layout.Controls.Add(btnOK, 1, 2);
        this.Controls.Add(layout);
        this.AcceptButton = btnOK;
    }
}