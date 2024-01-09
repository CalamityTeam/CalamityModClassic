using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons.Bumblebirb {
public class GildedProboscis : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Gilded Proboscis");
		//Tooltip.SetDefault("This spear ignores npc immunity frames\nHeals the player on enemy hits");
	}

	public override void SetDefaults()
	{
		Item.width = 66;  //The width of the .png file in pixels divided by 2.
		Item.damage = 280;  //Keep this reasonable please.
		Item.DamageType = DamageClass.Melee/* tModPorter Suggestion: Consider MeleeNoSpeed for no attack speed scaling */;  //Dictates whether this is a melee-class weapon.
		Item.noMelee = true;
		Item.useTurn = true;
		Item.noUseGraphic = true;
		Item.useAnimation = 19;
		Item.useStyle = ItemUseStyleID.Shoot;
		Item.useTime = 19;
		Item.knockBack = 8.75f;  //Ranges from 1 to 9.
		Item.UseSound = SoundID.Item1;
		Item.autoReuse = true;  //Dictates whether the weapon can be "auto-fired".
		Item.height = 66;  //The height of the .png file in pixels divided by 2.
		Item.maxStack = 1;
		Item.value = 1000000;  //Value is calculated in copper coins.
		Item.shoot = Mod.Find<ModProjectile>("GildedProboscis").Type;
		Item.shootSpeed = 13f;
	}
	
	public override void ModifyTooltips(List<TooltipLine> list)
    {
        foreach (TooltipLine line2 in list)
        {
            if (line2.Mod == "Terraria" && line2.Name == "ItemName")
            {
                line2.OverrideColor = new Color(43, 96, 222);
            }
        }
    }
	
	public override bool CanUseItem(Player player)
    {
        for (int i = 0; i < 1000; ++i)
        {
            if (Main.projectile[i].active && Main.projectile[i].owner == Main.myPlayer && Main.projectile[i].type == Item.shoot)
            {
                return false;
            }
        }
        return true;
    }
}}
