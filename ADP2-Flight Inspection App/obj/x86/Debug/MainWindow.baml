   M S B A M L   `   `   `  ����U  QADP2-Flight Inspection App, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullG(clr-namespace:ADP2_Flight_Inspection_AppADP2_Flight_Inspection_App  5   	   +    %ADP2_Flight_Inspection_App.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   :local(clr-namespace:ADP2_Flight_Inspection_App     5   	     � Title$  
MainWindow��5   	   $	��450��6   $	��800��6)   .��5	      � ��5
   
   �� 5       �� ImageSource$l fhttps://www.rd.com/wp-content/uploads/2017/10/Why-Are-Commercial-Airplanes-White-_493536652-Jag_cz.jpg��6   .+�5   
   �� -   5        $��headline��5      $��Center=�6    $/��)Welcome to the ADP2 Flight Inspection App��6D   $��Center=�6x   $	��Top=�6�   $	��600��6�   $��Italic$�6�   $��124,23,69.6,0q�6�   $��30(�6�   $��light"�6�   $��50��6  �� 5   
   -   5         ��Name$ CSV_file��5      $��Italic$�6#   $
��Bold"�66   $��Select CSV file��6H   $
��Left=�6b   $��40��6}   $��
50,120,0,0q�6�   $	��Top=�6�   $	��187��6�   ���	#FF9EC2DA$y�	Collapsed=�6�   �� 5   
   -   5        $ XML_file��5      $��Select XML file��6#   $��Italic$�6=   $
��Bold"�6P   $
��Left=�6b   $��40��6}   $��
50,212,0,0q�6�   $	��Top=�6�   $	��187��6�   ���	#FF9EC2DA$y�	Collapsed=�6�   �� 5   
   -   5         ��Name$ CSVText��5      $
��Left=�6$   $��40��6?   $��281,120,0,0q�6K    �� TextWrapping$
 Wrap=�6`   $�� ��6t   $	��Top=�6|   $	��253��6�   $��Center=�6�   $y�	Collapsed=�6�   �� 5   
   -   5        $ XMLText��5      $
��Left=�6#   $��40��6>   $��281,215,0,0q�6J   $
 Wrap=�6_   $�� ��6s   $	��Top=�6{   $	��253��6�   $��Center=�6�   $y�	Collapsed=�6�   �� 5   
   -   5        $ Apply��5      $��Apply��6    $��Center=�60   $��60��6M   $��547,350,9.6,10q�6Y   $��Center=�6q   $	��237��6�   $��30(�6�   $��Light"�6�   #��� ��6�   $y�	Collapsed=�65  ��6�   Z� 65  �� 5   
   -   5        $ instructions��5      $��Italic$�6'   $
��Bold"�6:   $��Instructions��6L   $
��Left=�6c   $��40��6~   $��
50,301,0,0q�6�   $	��Top=�6�   $	��187��6�   ���	#FF9EC2DA$��15(�6�   $y�	Collapsed=�6  �� 5   
   -   5         ��Name$ filesInstruction��5       J� Opacity$	 0.7Q�6.   $
��Bold"�6<   $��Center=�6N   $��Center=�6k   $��18(�6�   $��Italic$�6�   #��� ��6�   NLPlease follow the instructions below and at the end press the finish button:�� 5      TR* Copy your XML file to: C:\Program Files\FlightGear 2020.3.6\data\Protocol folder�� 5      [Y* After opening the Flight Gear Program, go to the settings and Copy the following lines:�� 5      75--generic=socket,in,10,127.0.0.1,5400,tcp,XMLFileName�� 5      
--fdm=null�� 5      DB(replace XMLFileName with the wanted file name, without the: .xml)�� 5       '%* Press the Fly button on the program�� 5"      !* Wait for the flight to upload5$      �� 5%   
   -	   5        $ finish��5%      $��finish��6!   $��Italic$�62   $	��100��6E   $��30��6Q   $��349,336,344.6,24q�6]   $��15(�6w   ���	#FFB1D4E76�   5&      5'      