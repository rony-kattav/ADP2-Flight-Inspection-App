   M S B A M L   `   `   `  ����U  QADP2-Flight Inspection App, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:ADP2_Flight_Inspection_AppADP2_Flight_Inspection_App  5   	   0    *ADP2_Flight_Inspection_App.MediaPlayerView   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   :local(clr-namespace:ADP2_Flight_Inspection_App     5   	   -   5          � Title$  Media Player��5	   	   $	��158��6   $	��786��6+   .��5      � $��-2,2,2.333,-1.333q�6   $	��128��6&   $	��Top=�63   ��5   
   �� 5       �� ImageSource$] Whttps://cdn1.iconfinder.com/data/icons/web-mobile-ui-interface/90/video_stop-01-512.png��6    � ColumnDefinitions 5   
   �� 5      $��8* �6   �� 5      $
��121* �6   �� 5      $
��534* �6   �� 5      $
��130* �6   .+�5   
   �� 
���   �$��1q�6%   $
��Left=�69   $	��127��6T   $	��Top=�6a   $	��792��6y   ���	#F7AEAEAE$��4��6�   $��0,0,-12.667,0q�6�   ._�5      �� -   5        $��
timeSlider��5      $��56.667,73.667,50,29.333q�6'    �� TickPlacement$ TopLeft=�6k    �� TickFrequency$ 50Q�6�    � Value 6�   �� *	VM_Time+ �� Mode$ TwoWay=� �� UpdateSourceTrigger$ PropertyChanged=�5      �� 5   
   -   5        $��timePresentor��5      $
��Left=�6'   $��38��6B    �� TextWrapping$
 Wrap=�6N   $��00:00��6b   $	��Top=�6o   $��64��6�   $��30.667,17,0,0q�6�   
�������$��20(�6�   	 �� 
IsReadOnly	 . $��3��6  ��6�   Z� 
 �� SelectionBrush
 6�   Z� 6  �� 5   
   $
��Left=�6   $��38��6-   $
 Wrap=�69   $��
Play speed��6M   $	��Top=�6_   $	��110��6w   $��347.333,17,0,0q�6�   $��20(�6�   	 . $��2��6  ��6�   Z� ��6�   Z� 
 6�   Z� 6  �� 5   
   -   5        $��speedBox��5      $
��Left=�6#   $	��Top=�6>   $��75��6V   $��457.333,17,0,0q�6a   $��38��6y   $��2��6�   $��20(�6�    �� SelectedValue$	 x 1��6�   $��2��6  ��6�   Z� ��6   Z� .@�5      �� $��x 0.5��6   6-   �� 5      $	��x 1��6   6+   �� 5      $��x 1.5��6   6-   �� 5      $	��x 2��6   6+   �� 5       $	��x 3��6   6+   5"      �� 5%   
   -   5         ��Name$
 stop��5%      $��1��6   $��Button��63   $
��Left=�6D   $	��Top=�6_   $��48��6w   $��2��6�   $��160.333,11,0,0q�6�   $��48��6�    J� RenderTransformOrigin$ 0.667,0.476&�6�   ��6�   Z� ��6�   Z� ��5&      �� 5'      $] Whttps://cdn1.iconfinder.com/data/icons/web-mobile-ui-interface/90/video_stop-01-512.png��6   5)      �� 5*   
   -   5        $
 play��5*      $��1��6   $��Button��63   $
��Left=�6D   $	��Top=�6_   $��48��6w   $��2��6�   $��25.333,11,0,0q�6�   $��48��6�   $ -1.403,0.517&�6�   ��6�   Z� ��6�   Z� ��5+      �� 5,      $] Whttps://cdn1.iconfinder.com/data/icons/web-mobile-ui-interface/90/video_play-01-512.png��6   5.      �� 5/   
   -   5        $ pause��5/      $��1��6    $��Button��64   $
��Left=�6E   $	��Top=�6`   $��48��6x   $��2��6�   $��93.333,11,0,0q�6�   $��48��6�   $ -2.542,0.503&�6�   ��6�   Z� ��6�   Z� ��50      �� 51      $^ Xhttps://cdn1.iconfinder.com/data/icons/web-mobile-ui-interface/90/video_pause-01-512.png��6   53      55      56      