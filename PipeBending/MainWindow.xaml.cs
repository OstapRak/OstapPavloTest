using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using devDept.Eyeshot;
using devDept.Graphics;
using devDept.Eyeshot.Entities;
using devDept.Geometry;
using Line = devDept.Eyeshot.Entities.Line;
using WpfApplication1.Commponents;
using WpfApplication1.ComponentsData;
using WpfApplication1.ViewModel;

namespace WpfApplication1
{

    public partial class MainWindow
    {

        private string componentName;
        

        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = new ViewModelMain();
            //this.DataContext = this;
            viewportLayout1.DisplayMode = displayType.Rendered;
            componentName = "";
            viewportLayout1.userControl = userControl;
            
            listBox.Items.Add("DShapeBending");
            listBox.Items.Add("RoundBending");
            listBox.Items.Add("DShapeDuct");
            listBox.Items.Add("RoundDuct");
            listBox.Items.Add("DShapeEndCap");
            listBox.Items.Add("DShapeEndCapWiithZipper");
            listBox.Items.Add("RoundEndCaps");
            listBox.Items.Add("RoundEndCapWithZipper");
            listBox.Items.Add("DShapeInlet");
            listBox.Items.Add("RoundConeInlet");
            listBox.Items.Add("RoundInlet");
            listBox.Items.Add("SquareDShapeInlet");
            listBox.Items.Add("SquareInlet");
            listBox.Items.Add("SquareRoundInlet");
            listBox.Items.Add("DShapeOutlet");
            listBox.Items.Add("RoundOutlet");
            listBox.Items.Add("DShapeReduction");
            listBox.Items.Add("RoundBottomExcentricReduction");
            listBox.Items.Add("RoundConcentricReduction");
            listBox.Items.Add("RoundExcentricReduction");
            listBox.Items.Add("VentilationUnitDShapeConnection");
            listBox.Items.Add("VentilationUnitRoundConnection");
            listBox.Items.Add("VentilationUnitSquareConnection");
            listBox.Items.Add("AirStreamLiner");
        }
      

    

        public void RefreshSelectedItem(object sender, TextChangedEventArgs e)
        {
            if ((DataContext as ViewModelMain).components.Count > 0&& NumberChangedComponent.Text != "-1")
            {
                int index = int.Parse(NumberChangedComponent.Text);
                viewportLayout1.MoveClick((DataContext as ViewModelMain).components[index]);
            }
            NumberChangedComponent.Text = "-1";
        }
    }
    public class MyViewportLayout : ViewportLayout
    {
        public PropertiesUserControl userControl;
        
        private int selectedComponent = -1;
        private int selectedComponentForObjectManipulator = -1;
        protected override void OnPreviewMouseDown(MouseButtonEventArgs e)
        {
            System.Drawing.Point location = RenderContextUtility.ConvertPoint(e.GetPosition(this));
            int index = GetEntityUnderMouseCursor(location);
            selectedComponent = -1;
            //SelectedIndexTextBox
            //SelectedIndexTextBox.Text = "-1";
            userControl.SetProperty(null);
            if (index >= 0)
            {
                try
                {
                    selectedComponent = int.Parse(Entities[index].EntityData.ToString());
                    userControl.SetProperty((DataContext as ViewModelMain).components[selectedComponent]);
                }
                catch
                {
                }
            }
            if (e.RightButton == MouseButtonState.Pressed)
            {
                if (ObjectManipulator.Visible)
                {
                    ObjectManipulator.Apply();

                    foreach (var entity in Entities)
                    {
                        entity.Selected = false;
                    }

                    (DataContext as ViewModelMain).components[selectedComponentForObjectManipulator].TransformationMatrix = (ObjectManipulator.FullTransformation * (new Transformation((DataContext as ViewModelMain).components[selectedComponentForObjectManipulator].TransformationMatrix))).Matrix;//.Matrix;
                }
                else if(selectedComponent>=0&& selectedComponent< (DataContext as ViewModelMain).components.Count)
                {
                    
                    string name = "" + selectedComponent;
                    foreach (var entity in Entities)
                    {
                        if (entity.EntityData as string == name || entity.EntityData as string == "cube" + name)
                        {
                            entity.Selected = true;
                        }
                    }
                    selectedComponentForObjectManipulator = selectedComponent;
                    ObjectManipulator.Enable(new Transformation((DataContext as ViewModelMain).components[selectedComponent].TransformationMatrix), true);
                    ObjectManipulator.ShowOriginalWhileEditing = false;
                    
                }
                
            }
            else if (e.LeftButton == MouseButtonState.Pressed&& (selectedComponent >= 0 && selectedComponent < (DataContext as ViewModelMain).components.Count))
            {
                if (!ObjectManipulator.Visible)
                {
                    
                    selectedComponent = int.Parse(Entities[index].EntityData.ToString());
                    
                    string name = "" + selectedComponent;
                    foreach (var entity in Entities)
                    {
                        if (entity.EntityData as string == "cube" + name)
                        {
                            if (entity.Visible == true)
                            {
                                entity.Visible = false;
                            }
                            else
                            {
                                entity.Visible = true;
                            }
                        }
                    }
                        
                }
            }
            base.OnPreviewMouseDown(e);
        }
        public void MoveClick(ComponentData component)
        {
            string name = "" + 0;
            foreach (var entity in Entities)
            {
                if (entity.EntityData as string == name || entity.EntityData as string == "cube" + name)
                {
                    entity.Selected = true;
                }
            }
            Entities.DeleteSelected();
            if (component != null)
            {
                foreach (var ent in ComponentFactory.GenerateRoundDuct(component as RoundDuctData))
                {
                    ent.TransformBy(new Transformation(component.TransformationMatrix));
                    ent.Translate(0, 0, 10);
                    Entities.Add(ent);
                }
                component.TransformationMatrix = ((new Translation(0, 0, 10)) * (new Transformation(component.TransformationMatrix))).Matrix;
            }
            OnPaint();
        }

        public void Add3DComponent(ComponentData component)
        {
            #region swich for components
            //switch (componentName)
            //{
            //    case "DShapeBending":
            //        c = new DShapeBending("" + nextIndex);
            //        break;
            //    case "RoundBending":
            //        c = new RoundBending("" + nextIndex);
            //        break;
            //    case "DShapeDuct":
            //        c = new DShapeDuct("" + nextIndex);
            //        break;
            //    case "RoundDuct":
            //        c = new RoundDuct("" + nextIndex);
            //        break;
            //    case "DShapeEndCap":
            //        c = new DShapeEndCap("" + nextIndex);
            //        break;
            //    case "DShapeEndCapWiithZipper":
            //        c = new DShapeEndCapWiithZipper("" + nextIndex);
            //        break;
            //    case "RoundEndCaps":
            //        c = new RoundEndCaps("" + nextIndex);
            //        break;
            //    case "RoundEndCapWithZipper":
            //        c = new RoundEndCapWithZipper("" + nextIndex);
            //        break;
            //    case "DShapeInlet":
            //        c = new DShapeInlet("" + nextIndex);
            //        break;
            //    case "RoundConeInlet":
            //        c = new RoundConeInlet("" + nextIndex);
            //        break;
            //    case "RoundInlet":
            //        c = new RoundInlet("" + nextIndex);
            //        break;
            //    case "SquareDShapeInlet":
            //        c = new SquareDShapeInlet("" + nextIndex);
            //        break;
            //    case "SquareInlet":
            //        c = new SquareInlet("" + nextIndex);
            //        break;
            //    case "SquareRoundInlet":
            //        c = new SquareRoundInlet("" + nextIndex);
            //        break;
            //    case "DShapeOutlet":
            //        c = new DShapeOutlet("" + nextIndex);
            //        break;
            //    case "RoundOutlet":
            //        c = new RoundOutlet("" + nextIndex);
            //        break;
            //    case "DShapeReduction":
            //        c = new DShapeReduction("" + nextIndex);
            //        break;
            //    case "RoundBottomExcentricReduction":
            //        c = new RoundBottomExcentricReduction("" + nextIndex);
            //        break;
            //    case "RoundConcentricReduction":
            //        c = new RoundConcentricReduction("" + nextIndex);
            //        break;
            //    case "RoundExcentricReduction":
            //        c = new RoundExcentricReduction("" + nextIndex);
            //        break;
            //    case "VentilationUnitDShapeConnection":
            //        c = new VentilationUnitDShapeConnection("" + nextIndex);
            //        break;
            //    case "VentilationUnitRoundConnection":
            //        c = new VentilationUnitRoundConnection("" + nextIndex);
            //        break;
            //    case "VentilationUnitSquareConnection":
            //        c = new VentilationUnitDShapeConnection("" + nextIndex);
            //        break;
            //    case "AirStreamLiner":
            //        c = new AirStreamLiner("" + nextIndex);
            //        break;
            #endregion

            if (component != null)
            {
                foreach (var ent in ComponentFactory.GenerateRoundDuct(component as RoundDuctData))
                {
                    Entities.Add(ent);

                }
                (DataContext as ViewModelMain).components.Add(component);
                (DataContext as ViewModelMain).nextIndex++;
            }
            OnPaint();
        }
    }
}