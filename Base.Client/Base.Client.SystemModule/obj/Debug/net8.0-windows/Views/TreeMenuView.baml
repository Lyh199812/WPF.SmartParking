   M S B A M L   `   `   `  ����S  OBase.Client.SystemModule, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullO,clr-namespace:Base.Client.SystemModule.ViewsBase.Client.SystemModule.Views  5      1    +Base.Client.SystemModule.Views.TreeMenuView   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      >local,clr-namespace:Base.Client.SystemModule.Views     5      \ XPrism.Unity.Wpf, Version=9.0.537.60525, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59V RPrism.Wpf, Version=9.0.537.60525, Culture=neutral, PublicKeyToken=40ee6c3a2184dc59&primshttp://prismlibrary.com/   5      !  Prism.Mvvm.ViewModelLocator    AutoWireViewModel  True5       � 	Resources 5       �� MergedDictionaries �� Source �� 
TargetType   Key  Iconfont J� RenderTransformOrigin �� Angle �� Triggers P� Property	 P� Value
 �� 
TargetName �� Property �� Value�� 5   
    5      �� 5      $S Mpack://application:,,,/Base.Client.Assets;component/Styles/BaseResources.xaml?�6%   �� 5      �� 5      " d�6&   ArrowButtonTemplate  .R�5      � $
��back��68   
������ .+�5      �� $��arrow��5   ?   $	����5   (   #��[ 68   $��Center=�5      $��Center=�67   $ 0.5,0.5&�5      
�������z�5   "   �� 5   &   $ 90Q�66   5      5       5      P� 5       �@d�	IsChecked$
	 TrueY�6;   . �5   "   �� $
 arrow��6)    � z� 5   &   �� 5   *   $ 0Q�6:   5!   #   5"      P� 5#       �@J�IsMouseOver$
	 True��6=   . �5$   "   �� $

 back��6)    � �� �	#110000006Q   5%      5'      5*      %    5,      .��5/      �  50   
    �� 
TargetType �� Mode-  'System.Windows.Input.KeyboardNavigation � ColumnDefinitions � RowDefinitions J� InputBindings d� MouseAction �� Command �� CommandParameter ��Name �� CornerRadius �� Padding J� SnapsToDevicePixels ��Name d�Name �� 	ClickMode  ArrowButtonTemplate d� 	IsChecked �� RelativeSource ��Name ]� 
Conditions  �� Property! �� Value" �� Binding# �� Value��%�  'Q�       7  �� 51      " Q�6   .�52      ��  � ��
 ���� 6/   �� 53       �@Q�
IsExpanded 6/   �� *
IsExpanded+$ TwoWay=�6[   �� 54       �@Q�
IsSelected 6/   �� *
IsSelected+$ TwoWay=��� 55       �@ AcceptsReturn$
 True��6E   �� 56       � ��$
 Left=�6?   �� 57       � ��$ Center=�6=   �� 58       � ��$	 8,5q�6,   �� 59       � ��
 �����6/   �� 5;       � ��$ 12(�6-   �� 5<       � ��$ 0q�64   �� 5=       � �� 5>      �� 5?      " Q�6*   .R�5@      � $
��root��6<   
������  5A   "   �� 5B   &   �� 5C   &   $��30��67    5E   "   �� 5F   &   $
��Auto �64   $��36��6B   �� 5G   &    5I   "   d� 5J   &   $ LeftDoubleClicke�63    5K   ?   �� *OpenViewCommand+ 5L   ?   �� .+�5O   "   �� $ Bd��6)   #��z��65   #��z��5P   1   #��z��5Q   1   $ 0��5R   1   $��2��6B   # z��6V   $
 true��5S   1   ._�5T   &   �� $ PART_Header��67   $��Header��6L   #��z��5U   ?   #��z��5V   ?   # z 5W   ?   5X   '   5Y   #   d� 5Z   "   $ Expander��6/   $��1��6A   $ Press=�6Q   8��   5[   7   �� *
IsExpanded+ �� *TemplatedParent+5\   7   �� 5]   "   $ 	ItemsHost��61   $��18,0,0,0q�6D   $��2��6V   $��1��6j   5^       5b      P� 5c   "    �@Q�
IsExpanded$	 false��6@   . �5d   &   �� $
 	ItemsHost��6C    � y�$ 	Collapsed=�6Z   5e   #   P� 5f   "    �@��HasItems$	 false��6>   . �5g   &   �� $
 Expander��6C    � y�$ Hidden=�6Y   5h   #   P� 5i   "    �@Q�
IsSelected$
	 true��6@   . �5j   &   �� $
 Bd��6C    � �� �#F7F9FA6S   �� 5k   &    � �� �#0b3d906C   �� 5l   &   $
 Expander��6S    � �� �#0b3d905m   #   ]� 5o   "    5p   &   �� 5q   *     �@Q�
IsSelected$
! true��6J   �� 5r   *     �@Q�IsSelectionActive$! false��6Q   ./�5t   &   �� $
 Bd��6C    � �� �	#FF0ABEFF6S   �� 5u   &    � �� �	#FFFFFFFF6C   5v   #   �� 5w   "   " 6.   �� *
MenuIcon+# 6K   Z� .a�5x   &   �� $

 root��6P    � ��$ 10,0,0,0q�6?   6P   5y   #   5{      5}      5~      5      .+�5�   
   R� $��Hidden=�61   
������ $��0q�5�      ��5�      �� *Menus+��5�      �� 5�      $ �� ItemsSource$ 6+   �� *
Children+.R�5�      �  5�      �� 5�      % �Name$% c1��6:   $��30 �6/   6:   �� 5�      .+�5�      �� #���  5�   $   $��Center=�6L   $��Center=�6g   $��18(�5�   $   $
 True��62   ��5�   $   �� *
MenuIcon+5�   2   �� 5�      $��1��6@   $��5,0,0,0q�6P   $��13(�6a   ��6$   �� *
MenuHeader+6a   5�      & �� Triggers& 5�      �� 5�      " 6&   �� *
MenuIcon+# 6C   Z� .a�5�      �� $
 c1��6%    � ��$ 0 �6F   5�      5�      5�      5�      5�      