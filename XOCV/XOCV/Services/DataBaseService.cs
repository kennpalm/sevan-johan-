using System.Collections.Generic;
using System.Linq;
using SQLite.Net;
using SQLiteNetExtensions.Extensions;
using Xamarin.Forms;
using XOCV.Models;
using XOCV.Models.ResponseModels;

namespace XOCV.Services
{
    public class DataBaseService
    {
        static object locker = new object();
        SQLiteConnection database;

        public DataBaseService()
        {
            database = DependencyService.Get<ISQLite>().GetConnection();
            // create the tables
            //database.CreateTable<ContentModel>(); 
            database.DropTable<ComplexFormsModel>();
            database.DropTable<FormModel>();
            database.CreateTable<ComplexItem>();
            database.DropTable<Item>();
            database.DropTable<PropertyModel>();
            database.DropTable<StoreNumberModel>();
            database.DropTable<SurveyorModel>();
            database.DropTable<CaptureModel>();

            database.CreateTable<ComplexFormsModel>();
            database.CreateTable<FormModel>();
            database.CreateTable<ComplexItem>();
            database.CreateTable<Item>();
            database.CreateTable<PropertyModel>();
            database.CreateTable<StoreNumberModel>();
            database.CreateTable<SurveyorModel>();
            database.CreateTable<CaptureModel>();
        }

        public IEnumerable<ComplexFormsModel> GetItems()
        {
            lock (locker)
            {
                
                var it = database.GetAllWithChildren<ComplexFormsModel>(recursive: true);
                var Form = database.GetAllWithChildren<FormModel>();
                var Complex = database.GetAllWithChildren<ComplexItem>();
                var itms = database.GetAllWithChildren<PropertyModel>();
                foreach (var a in it)
                {
                    foreach (var b in a.Forms)
                    {
                        foreach (var c in b.ComplexItems)
                        {
                            foreach (var d in c.Items)
                            {
                                d.Properties.PropertyModelId = itms.Find(x => x.PropertyModelId == d.PropertyModelId).PropertyModelId;
                                d.Properties.Text = itms.Find(x => x.PropertyModelId == d.PropertyModelId).Text;
                                //d.Properties = itms.First(x=> x.PropertyModelId == d.PropertyModelId);
                            }
                        }
                    }
                }
                return it;
            }
        }


        public ComplexFormsModel GetItem(int id)
        {
            lock (locker)
            {
                return database.Table<ComplexFormsModel>().FirstOrDefault(x => x.ComplexFormsModelID == id);
            }
        }

        public void SaveItem(ComplexFormsModel item)
        {
            lock (locker)
            {
                //database.InsertAll(item.Captures);
                if (item.ComplexFormsModelID != 0)
                {
                    database.UpdateWithChildren(item);
                    //return item.ID;
                }
                else
                {
                    foreach (var form in item.Forms)
                    {
                        foreach (var compItem in form.ComplexItems)
                        {
                            foreach (var _item in compItem.Items)
                            {
                                PropertyModel pModel = new PropertyModel
                                {
                                    PropertyModelId = _item.Properties.PropertyModelId,
                                    Text = _item.Properties.Text
                                };
                                database.Insert(pModel);
                            }
                        }
                    }
                    database.InsertWithChildren(item, recursive: true);
                }
            }
        }

        public void SavePropItem(PropertyModel item)
        {
            lock (locker)
            {
                //database.InsertAll(item.Captures);
                if (item.PropertyModelId != 0)
                {
                    database.UpdateWithChildren(item);
                    //return item.ID;
                }
                else
                {
                    //PropertyModel pModel = new PropertyModel();
                    //database.Insert(pModel);
                    //item.Forms[0].ComplexItems[0].Items[0].Properties = pModel;
                    database.Insert(item);
                    
                    
                    
                    //database.InsertWithChildren(item, recursive: true);




                }
            }
        }

        public int DeleteItem(int id)
        {
            lock (locker)
            {
                return database.Delete<ComplexFormsModel>(id);
            }
        }
    }
}