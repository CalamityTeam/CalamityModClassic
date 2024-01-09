using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons {
public class ShadowboltStaff : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Shadowbolt Staff");
		//Tooltip.SetDefault("The more tiles and enemies the beam bounces off of or travels through the more damage the beam does");
		Item.staff[Item.type] = true;
	}

    public override void SetDefaults()
    {
        Item.damage = 380;
        Item.DamageType = DamageClass.Magic;
        Item.mana = 20;
        Item.width = 58;
        Item.height = 56;
        Item.useTime = 14;
        Item.useAnimation = 14;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true;
        Item.knockBack = 8f;
        Item.value = 800000;
        Item.UseSound = SoundID.Item72;
        Item.autoReuse = true;
        Item.shoot = Mod.Find<ModProjectile>("Shadowbolt").Type;
        Item.shootSpeed = 6f;
    }
    
    public override void ModifyTooltips(List<TooltipLine> list)
    {
        foreach (TooltipLine line2 in list)
        {
            if (line2.Mod == "Terraria" && line2.Name == "ItemName")
            {
                line2.OverrideColor = new Color(0, 255, 0);
            }
        }
    }
    
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "ArmoredShell", 3);
        recipe.AddIngredient(null, "Phantoplasm", 5);
        recipe.AddIngredient(ItemID.ShadowbeamStaff);
        recipe.AddTile(null, "DraedonsForge");
        recipe.Register();
    }
}}