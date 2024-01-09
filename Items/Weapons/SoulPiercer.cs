using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point2.Items;

namespace CalamityModClassic1Point2.Items.Weapons {
public class SoulPiercer : ModItem
{
	public override void SetStaticDefaults()
	{
		//DisplayName.SetDefault("Soul Piercer");
		Item.staff[Item.type] = true;
	}

    public override void SetDefaults()
    {
        Item.damage = 135;
        Item.DamageType = DamageClass.Magic;
        Item.mana = 19;
        Item.width = 60;
        Item.height = 60;
        Item.useTime = 18;
        Item.useAnimation = 18;
        Item.useStyle = ItemUseStyleID.Shoot;
        Item.noMelee = true;
        Item.knockBack = 8f;
        Item.value = 1350000;
        Item.UseSound = SoundID.Item73;
        Item.autoReuse = true;
        Item.shoot = Mod.Find<ModProjectile>("SoulPiercer").Type;
        Item.shootSpeed = 6f;
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
    
    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "CosmiliteBar", 12);
        recipe.AddTile(null, "DraedonsForge");
        recipe.Register();
    }
}}