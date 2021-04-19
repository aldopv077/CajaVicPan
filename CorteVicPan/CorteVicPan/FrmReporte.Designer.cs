namespace CorteVicPan
{
    partial class FrmReporte
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource3 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource4 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource5 = new Microsoft.Reporting.WinForms.ReportDataSource();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource6 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.rpwCorte = new Microsoft.Reporting.WinForms.ReportViewer();
            this.dsBDCorteVicPan = new CorteVicPan.dsBDCorteVicPan();
            this.TblCorteBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.TblCorteTableAdapter = new CorteVicPan.dsBDCorteVicPanTableAdapters.TblCorteTableAdapter();
            this.TblConteoBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.TblConteoTableAdapter = new CorteVicPan.dsBDCorteVicPanTableAdapters.TblConteoTableAdapter();
            this.TblFacturasBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.TblFacturasTableAdapter = new CorteVicPan.dsBDCorteVicPanTableAdapters.TblFacturasTableAdapter();
            this.TblValesBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.TblValesTableAdapter = new CorteVicPan.dsBDCorteVicPanTableAdapters.TblValesTableAdapter();
            this.TblRetirosBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.TblRetirosTableAdapter = new CorteVicPan.dsBDCorteVicPanTableAdapters.TblRetirosTableAdapter();
            this.TblUsuariosBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.TblUsuariosTableAdapter = new CorteVicPan.dsBDCorteVicPanTableAdapters.TblUsuariosTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.dsBDCorteVicPan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TblCorteBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TblConteoBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TblFacturasBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TblValesBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TblRetirosBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TblUsuariosBindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // rpwCorte
            // 
            this.rpwCorte.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "dsCorte";
            reportDataSource1.Value = this.TblCorteBindingSource;
            reportDataSource2.Name = "dsConteo";
            reportDataSource2.Value = this.TblConteoBindingSource;
            reportDataSource3.Name = "dsFacturas";
            reportDataSource3.Value = this.TblFacturasBindingSource;
            reportDataSource4.Name = "dsVales";
            reportDataSource4.Value = this.TblValesBindingSource;
            reportDataSource5.Name = "dsRetiros";
            reportDataSource5.Value = this.TblRetirosBindingSource;
            reportDataSource6.Name = "dsUsuarios";
            reportDataSource6.Value = this.TblUsuariosBindingSource;
            this.rpwCorte.LocalReport.DataSources.Add(reportDataSource1);
            this.rpwCorte.LocalReport.DataSources.Add(reportDataSource2);
            this.rpwCorte.LocalReport.DataSources.Add(reportDataSource3);
            this.rpwCorte.LocalReport.DataSources.Add(reportDataSource4);
            this.rpwCorte.LocalReport.DataSources.Add(reportDataSource5);
            this.rpwCorte.LocalReport.DataSources.Add(reportDataSource6);
            this.rpwCorte.LocalReport.ReportEmbeddedResource = "CorteVicPan.rpoCorte.rdlc";
            this.rpwCorte.Location = new System.Drawing.Point(0, 0);
            this.rpwCorte.Name = "rpwCorte";
            this.rpwCorte.Size = new System.Drawing.Size(308, 546);
            this.rpwCorte.TabIndex = 0;
            // 
            // dsBDCorteVicPan
            // 
            this.dsBDCorteVicPan.DataSetName = "dsBDCorteVicPan";
            this.dsBDCorteVicPan.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // TblCorteBindingSource
            // 
            this.TblCorteBindingSource.DataMember = "TblCorte";
            this.TblCorteBindingSource.DataSource = this.dsBDCorteVicPan;
            // 
            // TblCorteTableAdapter
            // 
            this.TblCorteTableAdapter.ClearBeforeFill = true;
            // 
            // TblConteoBindingSource
            // 
            this.TblConteoBindingSource.DataMember = "TblConteo";
            this.TblConteoBindingSource.DataSource = this.dsBDCorteVicPan;
            // 
            // TblConteoTableAdapter
            // 
            this.TblConteoTableAdapter.ClearBeforeFill = true;
            // 
            // TblFacturasBindingSource
            // 
            this.TblFacturasBindingSource.DataMember = "TblFacturas";
            this.TblFacturasBindingSource.DataSource = this.dsBDCorteVicPan;
            // 
            // TblFacturasTableAdapter
            // 
            this.TblFacturasTableAdapter.ClearBeforeFill = true;
            // 
            // TblValesBindingSource
            // 
            this.TblValesBindingSource.DataMember = "TblVales";
            this.TblValesBindingSource.DataSource = this.dsBDCorteVicPan;
            // 
            // TblValesTableAdapter
            // 
            this.TblValesTableAdapter.ClearBeforeFill = true;
            // 
            // TblRetirosBindingSource
            // 
            this.TblRetirosBindingSource.DataMember = "TblRetiros";
            this.TblRetirosBindingSource.DataSource = this.dsBDCorteVicPan;
            // 
            // TblRetirosTableAdapter
            // 
            this.TblRetirosTableAdapter.ClearBeforeFill = true;
            // 
            // TblUsuariosBindingSource
            // 
            this.TblUsuariosBindingSource.DataMember = "TblUsuarios";
            this.TblUsuariosBindingSource.DataSource = this.dsBDCorteVicPan;
            // 
            // TblUsuariosTableAdapter
            // 
            this.TblUsuariosTableAdapter.ClearBeforeFill = true;
            // 
            // FrmReporte
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(308, 546);
            this.Controls.Add(this.rpwCorte);
            this.Name = "FrmReporte";
            this.Text = "Reporte";
            this.Load += new System.EventHandler(this.FrmReporte_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dsBDCorteVicPan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TblCorteBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TblConteoBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TblFacturasBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TblValesBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TblRetirosBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TblUsuariosBindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer rpwCorte;
        private System.Windows.Forms.BindingSource TblCorteBindingSource;
        private dsBDCorteVicPan dsBDCorteVicPan;
        private System.Windows.Forms.BindingSource TblConteoBindingSource;
        private System.Windows.Forms.BindingSource TblFacturasBindingSource;
        private System.Windows.Forms.BindingSource TblValesBindingSource;
        private System.Windows.Forms.BindingSource TblRetirosBindingSource;
        private System.Windows.Forms.BindingSource TblUsuariosBindingSource;
        private dsBDCorteVicPanTableAdapters.TblCorteTableAdapter TblCorteTableAdapter;
        private dsBDCorteVicPanTableAdapters.TblConteoTableAdapter TblConteoTableAdapter;
        private dsBDCorteVicPanTableAdapters.TblFacturasTableAdapter TblFacturasTableAdapter;
        private dsBDCorteVicPanTableAdapters.TblValesTableAdapter TblValesTableAdapter;
        private dsBDCorteVicPanTableAdapters.TblRetirosTableAdapter TblRetirosTableAdapter;
        private dsBDCorteVicPanTableAdapters.TblUsuariosTableAdapter TblUsuariosTableAdapter;
    }
}