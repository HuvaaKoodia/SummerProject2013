using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System;
using System.Linq;

public class XMLsys : MonoBehaviour {
	
	public AbilitiesDatabase abiDB;

	void Start () {
		readXML();
	}
	
	void OnDestroy(){
		writeXML();
	}
		
	void readXML(){
		
		string path=Application.dataPath+@"\Data\Abilities";
		if (!Directory.Exists(path)){
			return;
		}
		
		var Xdoc=new XmlDocument();
		foreach (var a in abiDB.abilities){
			//open xml
			var file=@"\"+a.gameObject.name+".xml";
			if (!File.Exists(path+file)) continue;
			Xdoc.Load(path+file);
			
			//read ability
			var p_stats=a.GetComponent<ProjectileStats>();
			var a_stats=a.GetComponent<AbilityStats>();
			var u_stats=a.GetComponent<UpgradeStats>();
			
			//read xml
			var root=Xdoc["Stats"];
			
			var abis=root["Basic"];
			var pros=root["Values"];
			var ups=root["Upgrade"];
	
			//basic
			a_stats.Name=getStr(abis,"Name");
			a_stats.Cost=getInt(abis,"Cost");
			a_stats.Sprite=getStr(abis,"Sprite");
			
			//values
			p_stats.Speed=getFlt(pros,"Speed");
			p_stats.Power=getFlt(pros,"Power");
			p_stats.Knockback=getFlt(pros,"Knockback");
			p_stats.Life_time=getFlt(pros,"Lifetime");
			p_stats.EnergyCost=getFlt(pros,"Energycost");
			p_stats.Radius=getFlt(pros,"Radius");
			
			p_stats.Speed_multi=getFlt(pros,"SpeedMulti");
			p_stats.Power_multi=getFlt(pros,"PowerMulti");
			p_stats.Knockback_multi=getFlt(pros,"KnockbackMulti");
			p_stats.Life_time_multi=getFlt(pros,"LifetimeMulti");
			p_stats.EnergyCost_multi=getFlt(pros,"EnergycostMulti");
			p_stats.Radius_multi=getFlt(pros,"RadiusMulti");
			
			//upgrades
			List<UpgradeStat> upgrades=new List<UpgradeStat>();
			
			foreach (XmlElement u in ups){
				foreach (var e in System.Enum.GetValues(typeof(UpgradeStat))){
					if (e.ToString()==u.Name){
						if (u.InnerText.ToLower().StartsWith("t"))
							upgrades.Add((UpgradeStat)e);
					}
				}
			}
			u_stats.AvailableUpgrades=upgrades.ToArray();
		}
	/*
		try{
		catch(Exception e){
			Debug.Log("Oh noes teh XMLs are corrupt!");
		}*/
	}

	
	void writeXML(){
		string path=Application.dataPath+@"\Data\Abilities";
		if (!Directory.Exists(path)){
			Directory.CreateDirectory(path);
		}
		
		foreach (var a in abiDB.abilities){
			//read ability
			var p_stats=a.GetComponent<ProjectileStats>();
			var a_stats=a.GetComponent<AbilityStats>();
			var u_stats=a.GetComponent<UpgradeStats>();
			
			//create xml
			string file=@"\"+a.gameObject.name+".xml";
			var Xdoc=new XmlDocument();
			
			var root=Xdoc.CreateElement("Stats");
			
			var abis=Xdoc.CreateElement("Basic");
			var pros=Xdoc.CreateElement("Values");
			var ups=Xdoc.CreateElement("Upgrade");
			
			//abi stats
			addElement(Xdoc,abis,"Name",a_stats.Name);
			addElement(Xdoc,abis,"Sprite",a_stats.Sprite);
			addElement(Xdoc,abis,"Cost",a_stats.Cost);
			
			//pro stats
			
			addElement(Xdoc,pros,"Speed",p_stats.Speed);
			addElement(Xdoc,pros,"Power",p_stats.Power);
			addElement(Xdoc,pros,"Knockback",p_stats.Knockback);
			addElement(Xdoc,pros,"Lifetime",p_stats.Life_time);
			addElement(Xdoc,pros,"Energycost",p_stats.EnergyCost);
			addElement(Xdoc,pros,"Radius",p_stats.EnergyCost);
			
			addElement(Xdoc,pros,"SpeedMulti",p_stats.Speed_multi);
			addElement(Xdoc,pros,"PowerMulti",p_stats.Power_multi);
			addElement(Xdoc,pros,"KnockbackMulti",p_stats.Knockback_multi);
			addElement(Xdoc,pros,"LifetimeMulti",p_stats.Life_time_multi);
			addElement(Xdoc,pros,"EnergycostMulti",p_stats.EnergyCost_multi);
			addElement(Xdoc,pros,"RadiusMulti",p_stats.EnergyCost_multi);
			
			//up stats
			
			foreach (var u in System.Enum.GetNames(typeof(UpgradeStat))){
				string v="false";
				foreach(var au in u_stats.AvailableUpgrades){
					if (au.ToString()==u){
						v="true";
						break;
					}
				}
				addElement(Xdoc,ups,u,v);
			}

			//save xml
			Xdoc.AppendChild(root);
			
			root.AppendChild(abis);
			root.AppendChild(pros);
			root.AppendChild(ups);
			
			Xdoc.Save(path+file);
		}
	}
	
	
	//subs
	string getStr(XmlElement element,string name){
		return element[name].InnerText;
	}
	
	int getInt(XmlElement element,string name){
		return int.Parse(element[name].InnerText);
	}
	
	float getFlt(XmlElement element,string name){
		return float.Parse(element[name].InnerText);
	}
	
	void addElement(XmlDocument Xdoc,XmlElement element,string name,string val){
		var node=Xdoc.CreateElement(name);
		node.InnerText=val;
		element.AppendChild(node);
	}
	
	void addElement(XmlDocument Xdoc,XmlElement element,string name,int val){
		addElement(Xdoc,element,name,val.ToString());
	}
	
	void addElement(XmlDocument Xdoc,XmlElement element,string name,float val){
		addElement(Xdoc,element,name,val.ToString());
	}
}
