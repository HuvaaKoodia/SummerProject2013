using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System;
using System.Linq;

public class XMLsys : MonoBehaviour {
	
	public AbilitiesDatabase abiDB;
	public PlayerDatabase plrDB;
	public LevelDatabase lvlDB;
	
	//engine logic
	void Awake () {
		readXML();
	}
	
	void OnDestroy(){
		writeXML();
	}
	
	//game logic
	void readXML(){
		
		string path=Application.dataPath+@"\Data";
		string folder="";
		string file="";
		
		XmlDocument Xdoc;
		XmlElement root;
		
		file=@"\Player.xml";
		if (File.Exists(path+file)){
			//player
			var player=plrDB.PlayerPrefab.GetComponent<PlayerMain>();
			
			Xdoc=new XmlDocument();
			Xdoc.Load(path+file);
		
			root=Xdoc["Stats"];
			
			player.MAX_HP=getFlt(root,"HP");
			player.MAX_MP=getFlt(root,"MP");
			
			player.mp_regen_delay=getInt(root,"MPregenDelay");
			player.MP_regen_multi_normal=getFlt(root,"MPregenSpeed");
			player.MP_regen_add=getFlt(root,"MPregenAcceleration");
			
			player.acceleration=getFlt(root,"Acceleration");
			player.jump_speed=getFlt(root,"Jump");
			player.speed_max=getFlt(root,"Speed");
		}
		
		//abilities
		folder=@"\Abilities";
		if (Directory.Exists(path+folder)){
			Xdoc=new XmlDocument();
			foreach (var a in abiDB.abilities){
				//open xml
				file=@"\"+a.gameObject.name+".xml";
				if (!File.Exists(path+folder+file)) continue;
				Xdoc.Load(path+folder+file);
				
				//read ability
				var p_stats=a.GetComponent<ProjectileStats>();
				var a_stats=a.GetComponent<AbilityStats>();
				var u_stats=a.GetComponent<UpgradeStats>();
				
				//read xml
				root=Xdoc["Stats"];
				
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
				p_stats.HP=getFlt(pros,"Hp");
				
				p_stats.Speed_multi=getFlt(pros,"SpeedMulti");
				p_stats.Power_multi=getFlt(pros,"PowerMulti");
				p_stats.Knockback_multi=getFlt(pros,"KnockbackMulti");
				p_stats.Life_time_multi=getFlt(pros,"LifetimeMulti");
				p_stats.EnergyCost_multi=getFlt(pros,"EnergycostMulti");
				p_stats.Radius_multi=getFlt(pros,"RadiusMulti");
				p_stats.HP_multi=getFlt(pros,"HpMulti");
				
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
		}
		//levels
		
		folder=@"\Levels";
		if (Directory.Exists(path+folder)){
			foreach (var l in lvlDB.levels){
				var level=l.gameObject.GetComponent<LevelStats>();
				file=@"\"+l.name+".xml";
				
				Xdoc=new XmlDocument();
				Xdoc.Load(path+folder+file);
				
				root=Xdoc["Stats"];
				
				readAuto(root,level);
			}
		}
	}
	
	void writeXML(){
		
		string path=Application.dataPath+@"\Data";
		string folder="";
		string file="";
		
		XmlDocument Xdoc;
		XmlElement root;
		
		checkFolder(path);
		
		//player
		var player=plrDB.PlayerPrefab.GetComponent<PlayerMain>();
		
		file=@"\Player.xml";
		Xdoc=new XmlDocument();
		root=Xdoc.CreateElement("Stats");
		addElement(root,"HP",player.MAX_HP);
		addElement(root,"MP",player.MAX_MP);
		
		addElement(root,"MPregenDelay",player.mp_regen_delay);
		addElement(root,"MPregenSpeed",player.MP_regen_multi_normal);
		addElement(root,"MPregenAcceleration",player.MP_regen_add);
		
		addElement(root,"Acceleration",player.acceleration);
		addElement(root,"Jump",player.jump_speed);
		addElement(root,"Speed",player.speed_max);
		
		Xdoc.AppendChild(root);
		Xdoc.Save(path+folder+file);
		
		folder=@"\Abilities";
		checkFolder(path+folder);
		
		foreach (var a in abiDB.abilities){
			//read ability
			var p_stats=a.GetComponent<ProjectileStats>();
			var a_stats=a.GetComponent<AbilityStats>();
			var u_stats=a.GetComponent<UpgradeStats>();
			
			//create xml
			file=@"\"+a.gameObject.name+".xml";
			Xdoc=new XmlDocument();
			
			root=Xdoc.CreateElement("Stats");
			
			var abis=Xdoc.CreateElement("Basic");
			var pros=Xdoc.CreateElement("Values");
			var ups=Xdoc.CreateElement("Upgrade");
			
			//abi stats
			addElement(abis,"Name",a_stats.Name);
			addElement(abis,"Sprite",a_stats.Sprite);
			addElement(abis,"Cost",a_stats.Cost);
			
			//pro stats
			
			addElement(pros,"Speed",p_stats.Speed);
			addElement(pros,"Power",p_stats.Power);
			addElement(pros,"Knockback",p_stats.Knockback);
			addElement(pros,"Lifetime",p_stats.Life_time);
			addElement(pros,"Energycost",p_stats.EnergyCost);
			addElement(pros,"Radius",p_stats.Radius);
			addElement(pros,"Hp",p_stats.HP);
			
			addElement(pros,"SpeedMulti",p_stats.Speed_multi);
			addElement(pros,"PowerMulti",p_stats.Power_multi);
			addElement(pros,"KnockbackMulti",p_stats.Knockback_multi);
			addElement(pros,"LifetimeMulti",p_stats.Life_time_multi);
			addElement(pros,"EnergycostMulti",p_stats.EnergyCost_multi);
			addElement(pros,"RadiusMulti",p_stats.Radius_multi);
			addElement(pros,"HpMulti",p_stats.HP_multi);
			
			//up stats
			foreach (var u in System.Enum.GetNames(typeof(UpgradeStat))){
				string v="false";
				foreach(var au in u_stats.AvailableUpgrades){
					if (au.ToString()==u){
						v="true";
						break;
					}
				}
				addElement(ups,u,v);
			}

			//save xml
			Xdoc.AppendChild(root);
			
			root.AppendChild(abis);
			root.AppendChild(pros);
			root.AppendChild(ups);
			
			Xdoc.Save(path+folder+file);
		}
		
		//levels
		folder=@"\Levels";
		checkFolder(path+folder);
		
		foreach (var l in lvlDB.levels){
			var level=l.gameObject.GetComponent<LevelStats>();
			file=@"\"+l.name+".xml";
			Xdoc=new XmlDocument();
			root=Xdoc.CreateElement("Stats");
			
			writeAuto(root,level);
			
			Xdoc.AppendChild(root);
			Xdoc.Save(path+folder+file);
		}
	}
	
	
	//subs
	string getStr(XmlElement element,string name){
		if (element[name]==null) return "";
		return element[name].InnerText;
	}
	
	int getInt(XmlElement element,string name){
		if (element[name]==null) return 0;
		return int.Parse(element[name].InnerText);
	}
	
	float getFlt(XmlElement element,string name){
		if (element[name]==null) return 0f;
		return float.Parse(element[name].InnerText);
	}
	
	void addElement(XmlElement element,string name,string val){
		var node=element.OwnerDocument.CreateElement(name);
		node.InnerText=val;
		element.AppendChild(node);
	}
	
	void addElement(XmlElement element,string name,int val){
		addElement(element,name,val.ToString());
	}
	
	void addElement(XmlElement element,string name,float val){
		addElement(element,name,val.ToString());
	}
		
	void readAuto(XmlElement element,object obj){
		foreach (var f in obj.GetType().GetFields()){
			if (f.IsPublic){
				if (element[f.Name]!=null){
					f.SetValue(obj,Convert.ChangeType(element[f.Name].InnerText,f.FieldType));
				}
			}
		}
	}
	
	void writeAuto(XmlElement element,object obj){
		foreach (var f in obj.GetType().GetFields()){
			addElement(element,f.Name,f.GetValue(obj).ToString());
		}
	}
	
	/// <summary>
	/// Creates a folder if it doesn't exist
	/// </summary>
	void checkFolder(string path){
		if (!Directory.Exists(path)){
			Directory.CreateDirectory(path);
		}
	}
}
