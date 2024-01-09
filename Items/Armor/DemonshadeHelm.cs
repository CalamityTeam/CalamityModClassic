using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items.Armor;

namespace CalamityModClassic1Point2.Items.Armor 
{
	[AutoloadEquip(EquipType.Head)]
	public class DemonshadeHelm : ModItem
	{
        public override void SetStaticDefaults()
        {
            //DisplayName.SetDefault("Demonshade Helm");
            //Tooltip.SetDefault("50% increased damage and crit. chance and +20 max minions");
        }

	    public override void SetDefaults()
	    {
	        Item.width = 18;
	        Item.height = 18;
	        Item.value = 10000000;
	        Item.defense = 55; //15
	    }
	    
	    public override void ModifyTooltips(List<TooltipLine> list)
	    {
	        foreach (TooltipLine line2 in list)
	        {
	            if (line2.Mod == "Terraria" && line2.Name == "ItemName")
	            {
	                line2.OverrideColor = new Color(255, 0, 255);
	            }
	        }
	    }
	
	    public override bool IsArmorSet(Item head, Item body, Item legs)
	    {
	        return body.type == Mod.Find<ModItem>("DemonshadeBreastplate").Type && legs.type == Mod.Find<ModItem>("DemonshadeGreaves").Type;
	    }
	    
	    public override void ArmorSetShadows(Player player)
	    {
	    	player.armorEffectDrawShadow = true;
	    	player.armorEffectDrawOutlines = true;
	    }
	
	    public override void UpdateArmorSet(Player player)
	    {
	        player.setBonus = "Melee attacks inflict shadowflame\n" +
	        	"Shadowbeams and demon scythes will fire down when you are hit\n" +
	        	"A friendly red devil follows you around";
	        CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>();
	        modPlayer.dsSetBonus = true;
	        modPlayer.redDevil = true;
			if (player.whoAmI == Main.myPlayer)
			{
				if (player.FindBuffIndex(Mod.Find<ModBuff>("RedDevil").Type) == -1)
				{
					player.AddBuff(Mod.Find<ModBuff>("RedDevil").Type, 3600, true);
				}
				if (player.ownedProjectileCounts[Mod.Find<ModProjectile>("RedDevil").Type] < 1)
				{
					Projectile.NewProjectile(player.GetSource_FromThis(), player.Center.X, player.Center.Y, 0f, -1f, Mod.Find<ModProjectile>("RedDevil").Type, 0, 0f, Main.myPlayer, 0f, 0f);
				}
			}
	    }
	    
	    public override void UpdateEquip(Player player)
	    {
	    	player.maxMinions += 20;
			player.GetDamage(DamageClass.Melee) += 0.5f;
	       	player.GetDamage(DamageClass.Throwing) += 0.5f;
		    player.GetDamage(DamageClass.Ranged) += 0.5f;
	        player.GetDamage(DamageClass.Magic) += 0.5f;
	        player.GetDamage(DamageClass.Summon) += 0.5f;
	   	    player.GetCritChance(DamageClass.Melee) += 50;
			player.GetCritChance(DamageClass.Magic) += 50;
			player.GetCritChance(DamageClass.Ranged) += 50;
			player.GetCritChance(DamageClass.Throwing) += 50;
	    }
	
	    public override void AddRecipes()
	    {
	        Recipe recipe = CreateRecipe();
	        recipe.AddIngredient(null, "ShadowspecBar", 40);
	        recipe.AddTile(null, "DraedonsForge");
	        recipe.Register();
	    }
	}
}