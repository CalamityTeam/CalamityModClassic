using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using CalamityModClassic1Point1.Items.Armor;

namespace CalamityModClassic1Point1.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class DemonshadeHelm : ModItem
{

    public override void SetDefaults()
    {
        //Tooltip.SetDefault("Demonshade Helm");
        Item.width = 18;
        Item.height = 18;
        ////Tooltip.SetDefault("50% increased melee damage, crit. chance, and swing speed\nShadowbeams and demon scythes will fire down while invincibility is active");
        Item.value = 5500000;
        Item.expert = true;
        Item.defense = 30; //15
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
        player.setBonus =("Melee attacks inflict shadowflame\nGreat for impersonating devs!");
        CalamityPlayer modPlayer = player.GetModPlayer<CalamityPlayer>();
        modPlayer.dsSetBonus = true;
    }
    
    public override void UpdateEquip(Player player)
    {
    	player.GetDamage(DamageClass.Melee) *= 1.5f;
        player.GetCritChance(DamageClass.Melee) += 50;
        player.GetAttackSpeed(DamageClass.Melee) *= 1.5f;
        if (player.immune)
		{
			if (Main.rand.Next(8) == 0)
			{
		 	    for (int l = 0; l < 1; l++)
				{
					float x = player.position.X + (float)Main.rand.Next(-400, 400);
					float y = player.position.Y - (float)Main.rand.Next(500, 800);
					Vector2 vector = new Vector2(x, y);
					float num15 = player.position.X + (float)(player.width / 2) - vector.X;
					float num16 = player.position.Y + (float)(player.height / 2) - vector.Y;
					num15 += (float)Main.rand.Next(-100, 101);
					int num17 = 22;
					float num18 = (float)Math.Sqrt((double)(num15 * num15 + num16 * num16));
					num18 = (float)num17 / num18;
					num15 *= num18;
					num16 *= num18;
					int num19 = Projectile.NewProjectile(player.GetSource_FromThis(), x, y, num15, num16, 294, 300, 7f, player.whoAmI, 0f, 0f);
					Main.projectile[num19].ai[1] = player.position.Y;
				}
		 	    for (int l = 0; l < 1; l++)
				{
					float x = player.position.X + (float)Main.rand.Next(-400, 400);
					float y = player.position.Y - (float)Main.rand.Next(500, 800);
					Vector2 vector = new Vector2(x, y);
					float num15 = player.position.X + (float)(player.width / 2) - vector.X;
					float num16 = player.position.Y + (float)(player.height / 2) - vector.Y;
					num15 += (float)Main.rand.Next(-100, 101);
					int num17 = 22;
					float num18 = (float)Math.Sqrt((double)(num15 * num15 + num16 * num16));
					num18 = (float)num17 / num18;
					num15 *= num18;
					num16 *= num18;
					int num19 = Projectile.NewProjectile(player.GetSource_FromThis(), x, y, num15, num16, 45, 500, 7f, player.whoAmI, 0f, 0f);
					Main.projectile[num19].ai[1] = player.position.Y;
				}
			}
        }
    }

    public override void AddRecipes()
    {
        Recipe recipe = CreateRecipe();
        recipe.AddIngredient(null, "ShadowspecBar", 4);
        recipe.AddIngredient(ItemID.SoulofFright, 10);
        recipe.AddTile(TileID.MythrilAnvil);
        recipe.Register();
    }
}}