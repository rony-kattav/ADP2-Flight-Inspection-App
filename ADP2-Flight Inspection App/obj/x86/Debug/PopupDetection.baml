   M S B A M L   `   `   `  ����U  QADP2-Flight Inspection App, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:ADP2_Flight_Inspection_AppADP2_Flight_Inspection_App  5      b ^PresentationFramework.Aero2, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35hKclr-namespace:Microsoft.Windows.Themes;assembly=PresentationFramework.Aero2Microsoft.Windows.Themes 5      /    )ADP2_Flight_Inspection_App.PopupDetection   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      :local(clr-namespace:ADP2_Flight_Inspection_App     5      ^ThemesKclr-namespace:Microsoft.Windows.Themes;assembly=PresentationFramework.Aero2     5      -   5          � Title$  PopupDetection��5      $��722.925��6   $��304.349��6.   .��5      � $��0,0,3,5.333q�6    � ColumnDefinitions 5   
   �� 5      $	��82* �6   �� 5      $
��476* �6   �� 5      $	��37* �6   .+�5   
   �� -   5         ��Name$ detectionList��5   (   $��0,0,0.333,0q�6   $��3��6@   ���	#FFD0F9FF �� View 5      �� 5      .P�5      �� $��	Feature 1��6&    �� Width$	 230��69    �� DisplayMemberBinding 6E   �� *
Feature1+ �� HeaderContainerStyle 5      �� 5       �� 
TargetType" ��6$   .�5   "   ��  �� Property � ��	 �� Value	 �	#FF7D7DB56?   �� 5   "    � ��
	 �����6?   �� 5   "    � ��$
	 Bold"�6?   5      5      �� 5      $��	Feature 2��6%   $	 230��68    6D   �� *
Feature2+ 5       �� 5!      " ��6$   .�5"   "   ��  � ��	 �	#FF7D7DB56?   �� 5#   "    � ��
	 �����6?   �� 5$   "    � ��$
	 Bold"�6?   5%      5'      �� 5(      $
��Time��6%   $	 230��63    6?   �� *Time+ 5)      �� 5*      " ��6$   .�5+   "   ��  � ��	 �	#FF7D7DB56?   �� 5,   "    � ��
	 �����6?   �� 5-   "    � ��$
	 Bold"�6?   5.      50      51      53      �� 54   
   -   5        
 ��Name$
 DLLlist��54       �� Text$  switch detecting algorithm��6$   $
��Left=�6F   $	��Top=�6a    �� 
IsEditable .  �� 
IsReadOnly . $	��211��55   7   $��394,228,0,0q�6C   $��32��6X   $��15(�6e    �� SelectedValue$	 x 1��6t   $y�Visible=�6�   $��2��6�   $��1��6  $��Italic$�6  
���   �   ComboBoxStyle1#���   6A  ��6�   Z� ��6�   Z� 6A  �� 57   
   -   5         ��Name$ 	switchDLL��57      $'��!switch anomaly detector algorithm��6$   $��Center=�6Q   $	��Top=�6n   $	��199��6�   $��1��6�   $��394,228,22.333,0q�6�   $��22��6�   $��2��6�   ���	#FF7BD0D0$y�	Collapsed=�6  58      59      