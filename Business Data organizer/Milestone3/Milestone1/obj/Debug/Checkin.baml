   M S B A M L   `   `   `  ����E  AMilestone1, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:Milestone1
Milestone1  5   	   l hSystem.Windows.Controls.DataVisualization.Toolkit, Version=4.0.0.0, Culture=neutral, PublicKeyToken=null�{clr-namespace:System.Windows.Controls.DataVisualization.Charting;assembly=System.Windows.Controls.DataVisualization.Toolkit2System.Windows.Controls.DataVisualization.Charting 5   	       Milestone1.Checkin   5   	   U QWPFToolkit, Version=3.5.40128.1, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   *localclr-namespace:Milestone1     5   	   �chartingToolkit{clr-namespace:System.Windows.Controls.DataVisualization.Charting;assembly=System.Windows.Controls.DataVisualization.Toolkit     5   	     � Title$  Checkin��5	   	   $	��540��6   $
��1104��6&   .��5
      � .+�5   
   �� -   5         ��Name$ bus_id��5      $�� ��6    $
��Left=�6+   $��
69,414,0,0q�6F   $	��Top=�6Z   �� 5   
   -   5        $ label��5      $&�� Number of Check-ins per Month At��6   $
��Left=�6J   $	��Top=�6e   $��
316,10,0,0q�6}   >  8System.Windows.Controls.DataVisualization.Charting.Chart  5   
   -   5          Name$ checkinchart��5      $
��Left=�6    $��	10,45,0,0q�6;   $	��Top=�6N   $	��454��6f   $	��908��6s      Series. 5      E  ?System.Windows.Controls.DataVisualization.Charting.ColumnSeries  $
��Left=�6+   $	��382��6F   $	��Top=�6S   $	��773��6k   ?  9System.Windows.Controls.DataVisualization.Charting.Series   Title$ My Data��6w   H  BSystem.Windows.Controls.DataVisualization.Charting.DataPointSeries   DependentValuePath$ Value��6�      IndependentValuePath$	 Key��6�      ItemsSource 6�   �� 5      �� 5   
   -   5         ��Name$ checkinbutton��5      $��Check-In��6(   $
��Left=�6;   $��
923,45,0,0q�6V   $	��Top=�6j   $��94��6�   $��55��6�   
����� �6�   �� 5   
   -   5        $	 bid��5      $�� ��6   $
��Left=�6(   $��
518,10,0,0q�6C   $	��Top=�6W   5      5      